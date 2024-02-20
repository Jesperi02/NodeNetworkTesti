using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using static NodeNetworkTesti.ViewModels.IONodePortViewModel;

namespace NodeNetworkTesti.ViewModels
{
    public class IONodePortViewModel : PortViewModel
    {
        public enum PortType
        {
            Input, Output
        }

        static IONodePortViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new Views.IONodePortView(), typeof(IViewFor<IONodePortViewModel>));
        }

        public PortType portType
        {
            get => _portType;
            set => this.RaiseAndSetIfChanged(ref _portType, value);
        }
        private PortType _portType;
    }
}