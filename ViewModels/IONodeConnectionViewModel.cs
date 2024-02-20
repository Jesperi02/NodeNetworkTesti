using NodeNetwork.ViewModels;
using NodeNetworkTesti.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeNetworkTesti.ViewModels
{
    public class IONodeConnectionViewModel : ConnectionViewModel
    {
        static IONodeConnectionViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new IONodeConnectionView(), typeof(IViewFor<IONodeConnectionViewModel>));
        }

        public IONodeConnectionViewModel(NetworkViewModel parent, NodeInputViewModel input, NodeOutputViewModel output) : base(parent, input, output)
        {

        }
    }
}
