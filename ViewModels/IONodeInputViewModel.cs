using NodeNetwork;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static NodeNetworkTesti.ViewModels.IONodePortViewModel;
using NodeNetwork.ViewModels;

namespace NodeNetworkTesti.ViewModels
{
    public class IONodeInputViewModel : NodeInputViewModel
    {
        public List<XElement> Descendants = new List<XElement>();

        static IONodeInputViewModel()
        {
            NNViewRegistrar.AddRegistration(() => new NodeInputView(), typeof(IViewFor<IONodeInputViewModel>));
        }

        public IONodeInputViewModel()
        {
            this.Port = new IONodePortViewModel { portType = PortType.Input };

            this.PortPosition = PortPosition.Left;
        }
    }
}
