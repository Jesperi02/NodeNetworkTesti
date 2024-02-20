using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NodeNetworkTesti.ViewModels;
using ReactiveUI;
using static NodeNetworkTesti.ViewModels.IONodePortViewModel;
using NodeNetwork.Views;

namespace NodeNetworkTesti.Views
{
    public partial class IONodePortView : IViewFor<IONodePortViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(IONodePortViewModel), typeof(IONodePortView), new PropertyMetadata(null));

        public IONodePortViewModel ViewModel
        {
            get => (IONodePortViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IONodePortViewModel)value;
        }
        #endregion

        #region Template Resource Keys
        public const String InputPortTemplateKey = "InputPortTemplate";
        public const String OutputPortTemplateKey = "OutputPortTemplate";
        #endregion

        public IONodePortView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.PortView.ViewModel).DisposeWith(d);

                this.OneWayBind(ViewModel, vm => vm.portType, v => v.PortView.Template, GetTemplateFromPortType)
                    .DisposeWith(d);

                this.OneWayBind(ViewModel, vm => vm.IsMirrored, v => v.PortView.RenderTransform,
                    isMirrored => new ScaleTransform(isMirrored ? -1.0 : 1.0, 1.0))
                    .DisposeWith(d);
            });
        }

        public ControlTemplate GetTemplateFromPortType(PortType type)
        {
            switch (type)
            {
                case PortType.Input: return (ControlTemplate)Resources[InputPortTemplateKey];
                case PortType.Output: return (ControlTemplate)Resources[OutputPortTemplateKey];
                default: throw new Exception("Unsupported port type");
            }
        }
    }
}