﻿using DynamicData;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;



namespace NodeNetworkTesti.ViewModels.Nodes
{
    internal class DivisionNodeViewModel : NodeViewModel
    {
        static DivisionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<DivisionNodeViewModel>));
        }

        public ValueNodeInputViewModel<int?> Input1 { get; }
        public ValueNodeInputViewModel<int?> Input2 { get; }
        public ValueNodeOutputViewModel<int?> Output { get; }

        public DivisionNodeViewModel()
        {
            Name = "Divide";

            Input1 = new ValueNodeInputViewModel<int?>
            {
                Name = "A",
                Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input1);

            Input2 = new ValueNodeInputViewModel<int?>
            {
                Name = "B",
                Editor = new IntegerValueEditorViewModel()
            };
            Inputs.Add(Input2);

            var divide = this.WhenAnyValue(vm => vm.Input1.Value, vm => vm.Input2.Value)
                .Select(_ => Input1.Value != null && Input2.Value != null && Input2.Value != 0 ? Input1.Value / Input2.Value : null);

            Output = new ValueNodeOutputViewModel<int?>
            {
                Name = "A / B",
                Value = divide
            };
            Outputs.Add(Output);
        }
    }
}
