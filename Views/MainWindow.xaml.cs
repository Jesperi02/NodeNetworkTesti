﻿using ReactiveUI;
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
                    // Do something with each IO element
                    //Debug.WriteLine(io);



                    string nodeName = function.Element("GUI-NAME").Value;
                    string ioTagGuiName = function.Descendants("GUI-NAME").FirstOrDefault()?.Value; // ETSI TÄHÄN IO KORTIN GUINAME EI FUNCTION GUI NAME
                    //string outputName = function.Element("MEM-TYPE").Value;
                    //string outputValue = function.Element("VALUE").Value;
                    //string inputValue = function.Element("VALUE").Value;
                    int inputValueInt = 0;

                   /* if (inputValue.Length > 0)
                    {
                        inputValueInt = int.Parse(inputValue);
                    }
                   */

                    IoNodeViewModel functionModel = new IoNodeViewModel();
                    functionModel.Name = nodeName;
                    functionModel.Input1.Name = ioTagGuiName;
                    //functionModel.Output.Name = outputName;
                    functionModel.ValueEditor.SetValue(inputValueInt);
                   // ioModel.Output.Value = outputValue;
                    ViewModel.NetworkViewModel.Nodes.Add(functionModel);



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

   
