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
        public IntegerValueEditorViewModel OutputValueEditor { get; } = new IntegerValueEditorViewModel();

        public List<IntegerValueEditorViewModel> inputList { get; } = new List<IntegerValueEditorViewModel>();

        public ValueNodeInputViewModel<int?> Input1 { get; }
        public ValueNodeOutputViewModel<int?> Output { get; }


       

        public IoNodeViewModel()
        {
            Name = "IO";

            Input1 = new ValueNodeInputViewModel<int?>
            {
                Name = "INPUTNAME",
                Editor = ValueEditor

            };
            Inputs.Add(Input1);

            */

            Output = new ValueNodeOutputViewModel<int?>
            {
                
                Name = "OUTPUTNAME",
                Value = this.WhenAnyValue(vm => vm.OutputValueEditor.Value),
            };
            Outputs.Add(Output);
            

        }
        
        
        public void addInput(string name, int val)
        {
            IntegerValueEditorViewModel ValueEditor = new IntegerValueEditorViewModel();
            inputList.Add(ValueEditor);

            var input = new ValueNodeInputViewModel<int?>
            {
                Name = name,
                Editor = ValueEditor,
            };

            ValueEditor.SetValue(val);
            this.Inputs.Add(input);
        }

        public void SetValue(int pos, int val)
        {
            ValueEditor.SetValue(val);
        }

        public void addConnection(NetworkViewModel NVM, IoNodeViewModel node)
        {
            NodeOutputViewModel con1 = Output;
            NodeInputViewModel con2 = node.Input1;

            ConnectionViewModel newConnection = NVM.ConnectionFactory.Invoke(con2, con1);
            NVM.Connections.Add(newConnection);
        }
    }
}
