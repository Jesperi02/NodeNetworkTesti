using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;
using NodeNetworkTesti.ViewModels;
using NodeNetwork.Toolkit.Layout.ForceDirected;
using NodeNetwork.ViewModels;
using System.Security.Cryptography.X509Certificates;
using NodeNetworkTesti.ViewModels.Nodes;
using System.Xml.Linq;
using System.Diagnostics;
using DynamicData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Xml;
using System.Windows.Markup;
using NodeNetwork.Toolkit.ValueNode;
using Xceed.Wpf.Toolkit.Primitives;

namespace NodeNetworkTesti.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<MainViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(MainViewModel), typeof(MainWindow), new PropertyMetadata(null));

        public MainViewModel ViewModel
        {
            get => (MainViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel)value;
        }
        #endregion

        void autoLayout(object sender, RoutedEventArgs e)
        {
            ForceDirectedLayouter layouter = new ForceDirectedLayouter();
            var config = new Configuration
            {
                Network = ViewModel.NetworkViewModel,
            };
            layouter.Layout(config, 300);
        }

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel = new MainViewModel();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.ListViewModel, v => v.nodeList.ViewModel).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.NetworkViewModel, v => v.viewHost.ViewModel).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.ValueLabel, v => v.valueLabel.Content).DisposeWith(d);
            });
        }

        private void openButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "XML files (.xml)|*.xml|APG files (.apg)|*.apg|All files (*.*)|*.*"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;

                XDocument doc = XDocument.Load(filename);
                List<XElement> functionElements = doc.Descendants("FUNCTION").ToList();
                
                foreach (XElement function in functionElements)
                {
                    string nodeName = function.Element("GUI-NAME").Value; // FUNCTION GUI-NAME
                    string ioTagGuiName = function.Element("IOS").Descendants("IO").Descendants("GUI-NAME").FirstOrDefault()?.Value; //  IO KORTIN GUINAME 
                    string ioTagValue = function.Element("IOS").Descendants("IO").Descendants("VALUE").FirstOrDefault()?.Value; // IO KORTIN VALUE
                    
                    int inputValueInt = 0;

                    if (ioTagValue.Length > 0)
                    {
                        inputValueInt = int.Parse(ioTagValue);
                    }
                   

                    IoNodeViewModel functionModel = new IoNodeViewModel(); 
                    functionModel.Name = nodeName;

                    List<XElement> ioElements = function.Descendants("IO").ToList();
                    foreach (XElement io in ioElements) {
                        // tee juttuja ioilla
                        
                        string ioNimi = io.Element("GUI-NAME").Value;
                        string ioValue = io.Element("VALUE").Value;
                        string ioPos = io.Element("POS").Value;
                        string ioMemType = io.Element("MEM-TYPE").Value;
                        List<XElement> descendants = io.Descendants().ToList();

                        int ioValueInt = 0;
                        if (ioValue.Length > 0)
                        {
                            ioValueInt = int.Parse(ioValue);
                        }

                        // Onko määritelty Input / Output
                        string port = "";

                        if (io.Element("PORT") != null) 
                        {
                            port = io.Element("PORT").Value;
                        }
                        else // manuaalinen tarkistus
                        {
                            if (io == ioElements.Last() && ioMemType == "MI")
                            {
                                port = "Out";
                            }
                            else
                            {
                                port = "In";
                            }

                            io.Add(
                                new XElement("PORT", port)
                            );
                        }

                        if (port == "In")
                        {
                            functionModel.addInput(ioNimi, ioValueInt, descendants);
                        }
                        else // Output
                        {
                            functionModel.addOutput(ioNimi, ioValueInt, descendants);
                        }

                        
                    }
                    
                    
                    ViewModel.NetworkViewModel.Nodes.Add(functionModel);



                }

            }




        }





        private void saveButtonClick(object sender, RoutedEventArgs e)
        { 
             var dialog = new Microsoft.Win32.SaveFileDialog();
             dialog.FileName = "Document"; // Default file name
             dialog.DefaultExt = ".xml"; // Default file extension
             dialog.Filter = "XML files (.xml)|*.xml|APG files (.apg)|*.apg|All files (*.*)|*.*"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

           

             //List<IoNodeViewModel> nodeList = (List<IoNodeViewModel>) ViewModel.NetworkViewModel.Nodes;
            
            
            IoNodeViewModel ConvertNodeViewModelToIoNodeViewModel(NodeViewModel nodeViewModel)
            {
                
                IoNodeViewModel ioNodeViewModel = new IoNodeViewModel();
                ioNodeViewModel.Name = nodeViewModel.Name;
                
                return ioNodeViewModel;
            }

            


            //TÄMÄ TEKEE UUDEN XML TIEDOSTON
            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;

                // tekee uuden xdocumentint jossa roottina program tagi
                XDocument doc = new XDocument(new XElement("PROGRAM"));

                
                

                foreach (var nodeViewModel in ViewModel.NetworkViewModel.Nodes.Items)
                {
                    // tekee FUNCTION elementin jokaiselle GUI-NAMELLE
                    XElement functionElement = new XElement("FUNCTION");

                    // Kääntää  NodeViewModelin  IoNodeViewModeliksi
                    IoNodeViewModel ioNodeViewModel = ConvertNodeViewModelToIoNodeViewModel(nodeViewModel);

                    // Lisää GUI-NAME element FUNCTION alle
                    functionElement.Add(new XElement("GUI-NAME", ioNodeViewModel.Name));

                    // LIsää IOS element  FUNCTION alle
                    XElement iosElement = new XElement("IOS");
                    functionElement.Add(iosElement);

                    // Lisää IO element  IOS alle
                    XElement ioElement = new XElement("IO");
                    iosElement.Add(ioElement);
                    ioElement.Add(new XElement("Name", "testi"));
                    //testi että onko input lista tyhjä
                    if (ioNodeViewModel.Inputs.Count == 0) 
                    { 
                        Debug.WriteLine("List is empty!");
                    }
                    
                    foreach (var inputViewModel in ioNodeViewModel.inputList)
                    {
                        ioElement.Add(new XElement("GUI-NAME", inputViewModel.Name));
                        ioElement.Add(new XElement("VALUE", inputViewModel.Editor));
                        
                    }
                    

                    foreach (var outputViewModel in ioNodeViewModel.Outputs.Items)
                    {
                        ioElement.Add(new XElement("GUI-NAME", outputViewModel.Name));
                        ioElement.Add(new XElement("VALUE", outputViewModel.Editor));
                    }


                    // Lisää  FUNCTION element documentin rootiks
                    doc.Root.Add(functionElement);
                }
                
                // Save the new XML document
                doc.Save(filename);
            }

        }
        // LUKEE NODELISTAN JA TALLENTAA TIEDOT UUTEEN XML TIEDOSTOON // TEHDÄÄN PROGRAM TAGI JA SEN ALLE FUNCTIO SEKÄ IOT=?!!








        

       
    }




}

   
