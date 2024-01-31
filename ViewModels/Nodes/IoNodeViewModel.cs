using DynamicData;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using NodeNetworkTesti.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace NodeNetworkTesti.ViewModels.Nodes
{
    internal class IoNodeViewModel : NodeViewModel
    {
        static IoNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<IoNodeViewModel>));
        }
        public IntegerValueEditorViewModel ValueEditor { get; } = new IntegerValueEditorViewModel();

        public ValueNodeInputViewModel<int?> Input1 { get; }
        public ValueNodeOutputViewModel<int?> Output { get; }

        public IoNodeViewModel()
        {
            Name = "Io";

            Input1 = new ValueNodeInputViewModel<int?>
            {
                Name = "INPUTNAME",
                Editor = new IntegerValueEditorViewModel()

            };
            Inputs.Add(Input1);



            Output = new ValueNodeOutputViewModel<int?>
            {
                
                Name = "OUTPUTNAME",
                Value = this.WhenAnyValue(vm => vm.ValueEditor.Value),
            };
            Outputs.Add(Output);
        }
    }
}
