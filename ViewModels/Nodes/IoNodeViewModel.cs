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
        public IntegerValueEditorViewModel ValueEditor { get; } = new IntegerValueEditorViewModel();

        public ValueNodeInputViewModel<int?> Input1 { get; }
        public ValueNodeOutputViewModel<int?> Output { get; }

        public List<ValueNodeInputViewModel<int?>> Inputs1 { get; } = new List<ValueNodeInputViewModel<int?>>();

       

        public IoNodeViewModel()
        {
            Name = "Io";
            /*
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
                Value = this.WhenAnyValue(vm => vm.ValueEditor.Value),
            };
            Outputs.Add(Output);

            foreach (var input in Inputs1)
            {
                var input1 = new ValueNodeInputViewModel<int?>
                {
                    Name = input.Name,
                    Editor = new IntegerValueEditorViewModel()

                };

                Debug.WriteLine("TESTI" + input.Name + input.Value);
                Inputs.Add(input);


            }


        }

        // UUTTA
        public void AddInput(string inputName,int? inputValue)
        {
            var input = new ValueNodeInputViewModel<int?>
            {
                Name = inputName,
                Editor = new IntegerValueEditorViewModel()
            };
            
            ValueEditor.SetValue(inputValue);
            Inputs.Add(input); // TÄMÄ LISÄÄ VAIN YHDEN NETWORKKIIN
            Inputs1.Add(input);

            


        }
        // UUTTA


        public void SetValue(int pos, int val)
        {
            ValueEditor.SetValue(val);
        }
        
    }
}
