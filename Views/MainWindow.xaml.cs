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
            layouter.Layout(config, 100);
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

        private void xmlButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "XML files (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;

                XDocument doc = XDocument.Load(filename);
                List<XElement> ioElements = doc.Descendants("IO").ToList();

                foreach (XElement io in ioElements)
                {
                    // Do something with each IO element
                    //Debug.WriteLine(io);



                    string inputName = io.Element("GUI-NAME").Value;
                    string outputName = io.Element("MEM-TYPE").Value;
                    string outputValue = io.Element("VALUE").Value;
                    string inputValue = io.Element("VALUE").Value;
                    int inputValueInt = 0;

                    if (inputValue.Length > 0)
                    {
                        inputValueInt = int.Parse(inputValue);
                    }


                    IoNodeViewModel ioModel = new IoNodeViewModel();
                    ioModel.Input1.Name = inputName;
                    ioModel.Output.Name = outputName;
                    //ioModel.ValueEditor.SetValue(inputValueInt);
                    ioModel.SetValue(0, inputValueInt);
                   // ioModel.Output.Value = outputValue;
                    ViewModel.NetworkViewModel.Nodes.Add(ioModel);



                }

            }




        }





        private void saveButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "XML files (.xml)|*.xml"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;
            }
        }

       
}




}

   
