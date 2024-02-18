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
            // Lists for connections
            Dictionary<int, IONodeInputViewModel> inputConDict = new Dictionary<int, IONodeInputViewModel>();
            List<IONodeInputViewModel> inputConList = new List<IONodeInputViewModel>();
            List<IONodeOutputViewModel> outputConList =  new List<IONodeOutputViewModel>();

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

                // create all nodes
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
                    foreach (XElement io in ioElements) { // create node inputs / outputs
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
                            IONodeInputViewModel input = functionModel.addInput(ioNimi, ioValueInt, descendants);

                            if (ioPos.Length > 0) // if we have a connection
                            {
                                inputConList.Add(input);
                            }
                        }
                        else // Output
                        {
                            IONodeOutputViewModel output = functionModel.addOutput(ioNimi, ioValueInt, descendants);

                            if (ioPos.Length > 0) // if we have a connection
                            {
                                outputConList.Add(output);
                            }
                        }
                    } // close io creation loop

                    ViewModel.NetworkViewModel.Nodes.Add(functionModel); // add node
                } // close node creation loop

                // create all connections
                foreach (IONodeOutputViewModel output in outputConList)
                {
                    
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

            //TÄMÄ TALLENTAA VALUE TAGIIN UUDEN ARVON
            /* 
             // Process save file dialog box results
             if (result == true)
             {
                // Save document
                string filename = dialog.FileName;
                string xmlFilePath = filename;

                XDocument doc = XDocument.Load(xmlFilePath);

                // Replace tag value
                foreach (var valueElement in doc.Descendants("VALUE"))
                {
                    if (valueElement != null)
                    {
                        valueElement.Value = "234324";
                    }
                }

                doc.Save(xmlFilePath);
            */

            List<IoNodeViewModel> nodeList = (List<IoNodeViewModel>) ViewModel.NetworkViewModel.Nodes;

            //TÄMÄ TEKEE UUDEN XML TIEDOSTON
            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;

                // Create a new XDocument with root element
                XDocument doc = new XDocument(new XElement("Root"));

                // Add an example VALUE element
                doc.Root.Add(new XElement("VALUE", "234324"));



                // Save the new XML document
                doc.Save(filename);
            }

        }









        

       
    }




}

   
