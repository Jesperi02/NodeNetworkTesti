using NodeNetworkTesti.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
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

namespace NodeNetworkTesti.Views
{
    /// <summary>
    /// Interaction logic for IONodePendingConnectionView.xaml
    /// </summary>
    public partial class IONodePendingConnectionView : IViewFor<IONodePendingConnectionViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(IONodePendingConnectionViewModel), typeof(IONodePendingConnectionView), new PropertyMetadata(null));

        public IONodePendingConnectionViewModel ViewModel
        {
            get => (IONodePendingConnectionViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IONodePendingConnectionViewModel)value;
        }
        #endregion

        public IONodePendingConnectionView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                PendingConnectionView.ViewModel = this.ViewModel;
                d(Disposable.Create(() => PendingConnectionView.ViewModel = null));
            });
        }
    }
}
