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
    public class IONodePendingConnectionViewModel : PendingConnectionViewModel
    {
        static IONodePendingConnectionViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new IONodePendingConnectionView(), 
                typeof(IViewFor<IONodePendingConnectionViewModel>));
        }

        public IONodePendingConnectionViewModel(NetworkViewModel parent) : base(parent)
        {

        }
    }
}
