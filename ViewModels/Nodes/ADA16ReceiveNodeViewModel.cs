using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;

namespace NodeNetworkTesti.ViewModels.Nodes
{
    internal class ADA16ReceiveNodeViewModel : NodeViewModel
    {
        static ADA16ReceiveNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<ADA16ReceiveNodeViewModel>));
        }

        public IntegerValueEditorViewModel ValueEditor { get; } = new IntegerValueEditorViewModel();

        public ValueNodeOutputViewModel<int?> Output { get; }

        public ADA16ReceiveNodeViewModel()
        {
            this.Name = "ADA-16 CAN RECEIVE";

            Output = new ValueNodeOutputViewModel<int?>
            {
                Name = "MI 0",
                // Editor = ValueEditor,
                Value = this.WhenAnyValue(vm => vm.ValueEditor.Value)
            };
            this.Outputs.Add(Output);
        }


    }
}
