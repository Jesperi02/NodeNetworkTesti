using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NodeNetwork.Views;
using DynamicData;
using static NodeNetworkTesti.ViewModels.IONodePortViewModel;

namespace NodeNetworkTesti.ViewModels
{
    internal class IONodeOutputViewModel<T> : ValueNodeOutputViewModel<int?>
    {
        public List<XElement> Descendants = new List<XElement>();

        public int connectionPos = 0;

        static IONodeOutputViewModel()
        {
            NNViewRegistrar.AddRegistration(() => new NodeOutputView(), typeof(IViewFor<IONodeOutputViewModel<PortType>>));
        }

        public void addConnection(NetworkViewModel NVM, IONodeInputViewModel<PortType> input)
        {
            ConnectionViewModel newConnection = NVM.ConnectionFactory.Invoke(input, this);
            NVM.Connections.Add(newConnection);
        }

        public IONodeOutputViewModel(PortType type)
        {
            this.Port = new IONodePortViewModel { portType = type };

            this.PortPosition = PortPosition.Right;
        }
    }
}
