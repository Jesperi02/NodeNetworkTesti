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
using static NodeNetworkTesti.ViewModels.IONodePortViewModel;

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

        public List<IONodeInputViewModel<PortType>> inputList { get; } = new List<IONodeInputViewModel<PortType>>();
        public List<IONodeOutputViewModel<PortType>> outputList { get; } = new List<IONodeOutputViewModel<PortType>>();
        
        public IONodeInputViewModel<PortType> addInput(string name, int val, List<XElement> descendants)
        {
            IntegerValueEditorViewModel InputValueEditor = new IntegerValueEditorViewModel();
 
            IONodeInputViewModel<PortType> input = new IONodeInputViewModel<PortType>(PortType.Input)
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

        public IONodeOutputViewModel<PortType> addOutput(string name, int val, int pos, List<XElement> descendants)
        {
            IntegerValueEditorViewModel OutputValueEditor = new IntegerValueEditorViewModel();
            
            IONodeOutputViewModel<PortType> output = new IONodeOutputViewModel<PortType>(PortType.Output)
            {
                Name = name,
                Editor = OutputValueEditor,
                Descendants = descendants,
                connectionPos = pos
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
}
