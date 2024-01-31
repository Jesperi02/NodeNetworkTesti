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
using DynamicData;
using System.Reactive.Linq;

namespace NodeNetworkTesti.ViewModels.Nodes
{
    internal class TestiNodeViewModel : NodeViewModel
    {
        static TestiNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<TestiNodeViewModel>));
        }

        public ValueNodeInputViewModel<int?> Input1 { get; }
        public ValueNodeInputViewModel<int?> Input2 { get; }
        public ValueNodeInputViewModel<int?> Input3 { get; }
        public ValueNodeInputViewModel<int?> Input4 { get; }
        public ValueNodeInputViewModel<int?> Input5 { get; }
        public ValueNodeInputViewModel<int?> Input6 { get; }
        public ValueNodeInputViewModel<int?> Input7 { get; }
        public ValueNodeInputViewModel<int?> Input8 { get; }
        public ValueNodeInputViewModel<int?> Input9 { get; }
        public ValueNodeInputViewModel<int?> Input10 { get; }
        public ValueNodeInputViewModel<int?> Input11 { get; }
        public ValueNodeInputViewModel<int?> Input12 { get; }
        public ValueNodeInputViewModel<int?> Input13 { get; }
        public ValueNodeInputViewModel<int?> Input14 { get; }
        public ValueNodeOutputViewModel<int?> Output { get; }

        public TestiNodeViewModel()
        {
            Name = "TESTI";

            Input1 = new ValueNodeInputViewModel<int?>
            {
                Name = "SLP",
                //Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input1);

            Input2 = new ValueNodeInputViewModel<int?>
            {
                Name = "SLP",
                //Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input2);

            Input3 = new ValueNodeInputViewModel<int?>
            {
                Name = "VA",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input3);

            Input4 = new ValueNodeInputViewModel<int?>
            {
                Name = "VS",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input4);

            Input5 = new ValueNodeInputViewModel<int?>
            {
                Name = "VST",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input5);

            Input6 = new ValueNodeInputViewModel<int?>
            {
                Name = "DZH",
                //Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input6);

            Input7 = new ValueNodeInputViewModel<int?>
            {
                Name = "DZL",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input7);

            Input8 = new ValueNodeInputViewModel<int?>
            {
                Name = "O",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input8);

            Input9 = new ValueNodeInputViewModel<int?>
            {
                Name = "A/M",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input9);

            Input10 = new ValueNodeInputViewModel<int?>
            {
                Name = "MS",
                //Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input10);

            Input11 = new ValueNodeInputViewModel<int?>
            {
                Name = "S",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input11);

            Input12 = new ValueNodeInputViewModel<int?>
            {
                Name = "M",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input12);

            Input13 = new ValueNodeInputViewModel<int?>
            {
                Name = "P",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input13);

            Input14 = new ValueNodeInputViewModel<int?>
            {
                Name = "I",
               // Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input14);



            var sum = this.WhenAnyValue(vm => vm.Input1.Value, vm => vm.Input2.Value)
                .Select(_ => Input1.Value != null && Input2.Value != null ? Input1.Value + Input2.Value : null);

            Output = new ValueNodeOutputViewModel<int?>
            {
                Name = "C",
                Value = sum
            };

            this.Outputs.Add(Output);

            void SetValue(int val)
            {
                //Tässä oli jeren muutkosia!!!!!!
                //ValueEditor.SetValue(val);
            }

            void addConnection(NetworkViewModel NVM, TestiNodeViewModel node)
            {
                //NodeInputViewModel con1 = this.Outputs.Items
                //ConnectionViewModel newConnection = NVM.ConnectionFactory.Invoke(this, node);
            }
        }

    }
}
