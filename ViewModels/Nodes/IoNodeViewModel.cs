using DynamicData;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using NodeNetworkTesti.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Xceed.Wpf.Toolkit.Primitives;

namespace NodeNetworkTesti.ViewModels.Nodes
{
    internal class IoNodeViewModel : NodeViewModel
    {
        static IoNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<IoNodeViewModel>));
        }
        public List<ValueNodeInputViewModel<int?>> inputList { get; } = new List<ValueNodeInputViewModel<int?>>();

        public List<ValueNodeOutputViewModel<int?>> outputList { get; } = new List<ValueNodeOutputViewModel<int?>>();

        public IoNodeViewModel()
        {
            Name = "IO";
        }
        
        
        public void addInput(string name, int val)
        {
            IntegerValueEditorViewModel InputValueEditor = new IntegerValueEditorViewModel();

            ValueNodeInputViewModel<int?> input = new ValueNodeInputViewModel<int?>
            {
                Name = name,
                Editor = InputValueEditor,
            };

            inputList.Add(input);

            InputValueEditor.SetValue(val);
            this.Inputs.Add(input);
        }

        public void addOutput(string name, int val)
        {
            IntegerValueEditorViewModel OutputValueEditor = new IntegerValueEditorViewModel();

            ValueNodeOutputViewModel<int?> Output = new ValueNodeOutputViewModel<int?>
            {
                Name = name,
                Editor = OutputValueEditor,
            };

            OutputValueEditor.SetValue(val);
            this.Outputs.Add(Output);
        }

        public void SetValue(int pos, int val)
        {
            //inputList[pos].SetValue(val);
        }

        public void addConnection(NetworkViewModel NVM, IoNodeViewModel node, int outPos, int inPos)
        {
            NodeOutputViewModel con1 = outputList[outPos];
            NodeInputViewModel con2 = node.inputList[inPos];

            ConnectionViewModel newConnection = NVM.ConnectionFactory.Invoke(con2, con1);
            NVM.Connections.Add(newConnection);
        }
    }
}
