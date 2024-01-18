using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using NodeNetwork.ViewModels;

namespace NodeNetworkTesti.ViewModels.Nodes
{
    internal class ADA16NodeViewModel : NodeViewModel
    {
        static ADA16NodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<ADA16NodeViewModel>));
        }

        public IntegerValueEditorViewModel ValueEditor { get; } = new IntegerValueEditorViewModel();

        public ValueNodeOutputViewModel<int?> Output { get; }

        public ADA16NodeViewModel()
        {
            this.Name = "ADA-16 EXP NTC10";

            Output = new ValueNodeOutputViewModel<int?>
            {
                Name = "Lämpötila, AI 1",
               // Editor = ValueEditor,
                Value = this.WhenAnyValue(vm => vm.ValueEditor.Value)
            };
            this.Outputs.Add(Output);
        }



    }
}
