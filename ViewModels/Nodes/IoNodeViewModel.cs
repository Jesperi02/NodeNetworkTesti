using DynamicData;
using NodeNetwork;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using NodeNetworkTesti.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using Xceed.Wpf.Toolkit.Primitives;

namespace NodeNetworkTesti.ViewModels.Nodes
{
    internal class IoNodeViewModel : NodeViewModel
    {
        static IoNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<IoNodeViewModel>));
        }

        public IoNodeViewModel()
        {
            Name = "IO";
        }

        public List<IONodeInputViewModel> inputList { get; } = new List<IONodeInputViewModel>();
        public List<IONodeOutputViewModel> outputList { get; } = new List<IONodeOutputViewModel>();
        
        public IONodeInputViewModel addInput(string name, int val, List<XElement> descendants)
        {
            IntegerValueEditorViewModel InputValueEditor = new IntegerValueEditorViewModel();
 
            IONodeInputViewModel input = new IONodeInputViewModel()
            {
                Name = name,
                Editor = InputValueEditor,
                Descendants = descendants
            };

            inputList.Add(input);
            InputValueEditor.SetValue(val);
            Inputs.Add(input);

            return input;
        }

        public IONodeOutputViewModel addOutput(string name, int val, List<XElement> descendants)
        {
            IntegerValueEditorViewModel OutputValueEditor = new IntegerValueEditorViewModel();
            
            IONodeOutputViewModel output = new IONodeOutputViewModel
            {
                Name = name,
                Editor = OutputValueEditor,
                Descendants = descendants
            };
            
            outputList.Add(output);
            OutputValueEditor.SetValue(val);
            Outputs.Add(output);

            return output;
        }

        public void SetValue(int pos, int val)
        {
            //inputList[pos].SetValue(val);
        }
    }

    internal class IONodeInputViewModel : ValueNodeInputViewModel<int?>
    {
        public List<XElement> Descendants = new List<XElement>();

        static IONodeInputViewModel()
        {
            NNViewRegistrar.AddRegistration(() => new NodeInputView(), typeof(IViewFor<IONodeInputViewModel>));
        }
    }

    internal class IONodeOutputViewModel : ValueNodeOutputViewModel<int?>
    {
        public List<XElement> Descendants = new List<XElement>();

        static IONodeOutputViewModel()
        {
            NNViewRegistrar.AddRegistration(() => new NodeOutputView(), typeof(IViewFor<IONodeOutputViewModel>));
        }

        public void addConnection(NetworkViewModel NVM, IONodeInputViewModel input)
        {
            ConnectionViewModel newConnection = NVM.ConnectionFactory.Invoke(input, this);
            NVM.Connections.Add(newConnection);
        }
    }
}
