﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using NodeNetworkTesti.ViewModels.Nodes;
using NodeNetworkTesti.Views;
using NodeNetwork;
using NodeNetwork.Toolkit;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using NodeNetworkTesti;
using ReactiveUI;
using NodeNetwork.Toolkit.Layout.ForceDirected;
using System.Windows;

namespace NodeNetworkTesti.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        static MainViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
        }

        public NodeListViewModel ListViewModel { get; } = new NodeListViewModel();
        public NetworkViewModel NetworkViewModel { get; } = new NetworkViewModel();

        #region ValueLabel
        private string _valueLabel;
        public string ValueLabel
        {
            get => _valueLabel;
            set => this.RaiseAndSetIfChanged(ref _valueLabel, value);
        }
        #endregion

        public MainViewModel()
        {
            ListViewModel.AddNodeType(() => new SumNodeViewModel());
            ListViewModel.AddNodeType(() => new SubtractionNodeViewModel());
            ListViewModel.AddNodeType(() => new ProductNodeViewModel());
            ListViewModel.AddNodeType(() => new DivisionNodeViewModel());
            ListViewModel.AddNodeType(() => new ConstantNodeViewModel());
            ListViewModel.AddNodeType(() => new TestiNodeViewModel());
            ListViewModel.AddNodeType(() => new ADA16NodeViewModel());
            ListViewModel.AddNodeType(() => new ADA16ReceiveNodeViewModel());

            IoNodeViewModel test1 = new IoNodeViewModel();
            IoNodeViewModel test2 = new IoNodeViewModel();
            
            NetworkViewModel.Nodes.Add(test1);
            NetworkViewModel.Nodes.Add(test2);

            test1.addConnection(NetworkViewModel, test2);

            NetworkViewModel.Validator = network =>
            {
                bool containsLoops = GraphAlgorithms.FindLoops(network).Any();
                if (containsLoops)
                {
                    return new NetworkValidationResult(false, false, new ErrorMessageViewModel("Network contains loops!"));
                }

                bool containsDivisionByZero = GraphAlgorithms.GetConnectedNodesBubbling(test1)
                    .OfType<DivisionNodeViewModel>()
                    .Any(n => n.Input2.Value == 0);
                if (containsDivisionByZero)
                {
                    return new NetworkValidationResult(false, true, new ErrorMessageViewModel("Network contains division by zero!"));
                }

                return new NetworkValidationResult(true, true, null);
            };

           


            



        }
    }
}