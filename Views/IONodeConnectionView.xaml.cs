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
    /// Interaction logic for IONodeConnectionView.xaml
    /// </summary>
    public partial class IONodeConnectionView : IViewFor<IONodeConnectionViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(IONodeConnectionViewModel), typeof(IONodeConnectionView), new PropertyMetadata(null));

        public IONodeConnectionViewModel ViewModel
        {
            get => (IONodeConnectionViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IONodeConnectionViewModel)value;
        }
        #endregion

        public IONodeConnectionView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                ConnectionView.ViewModel = this.ViewModel;
                d(Disposable.Create(() => ConnectionView.ViewModel = null));
            });
        }
    }
}
