using DynamicData;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace NodeNetworkTesti.ViewModels.Nodes
{
    internal class TestiNodeViewModel : NodeViewModel
    {

        static TestiNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<TestiNodeViewModel>));
        }

        public IntegerValueEditorViewModel ValueEditor { get; } = new IntegerValueEditorViewModel();

        public ValueNodeOutputViewModel<int?> Output { get; }

        public TestiNodeViewModel()
        {
            this.Name = "Testi";

            Output = new ValueNodeOutputViewModel<int?>
            {
                Name = "Mittarin arvo",
                Editor = ValueEditor,
                Value = this.WhenAnyValue(vm => vm.ValueEditor.Value)
            };

            this.Outputs.Add(Output);

            void SetValue(int val)
            {
                ValueEditor.SetValue(val);
            }

            void addConnection(NetworkViewModel NVM, TestiNodeViewModel node)
            {
                //NodeInputViewModel con1 = this.Outputs.Items
                //ConnectionViewModel newConnection = NVM.ConnectionFactory.Invoke(this, node);
            }
        }
    }
}
