using System.Windows;
using System.Windows.Controls;
using Umuna.Ui.Interop;
using Umuna.Ui.ViewModels;

namespace Umuna.Ui.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private ExternalAppHost? _host;

        public MainView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            vm.RequestStartHost += () =>
            {
                if (_host != null)
                    return;

                _host = new ExternalAppHost
                {
                    ExecutablePath = vm.ExecutablePath
                };

                MainGrid.Children.Add(_host);

                // Mark as running
                vm.IsHostRunning = true;
            };

            vm.RequestStopHost += () =>
            {
                if (_host != null)
                {
                    MainGrid.Children.Remove(_host);
                    _host.Dispose();
                    _host = null;

                    // Mark as not running
                    vm.IsHostRunning = false;
                }
            };
        }
    }
}
