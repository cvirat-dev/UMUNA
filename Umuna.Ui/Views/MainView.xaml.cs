using System.Windows.Controls;
using Umuna.Ui.Interop;

namespace Umuna.Ui.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            var host = new ExternalAppHost
            {
                ExecutablePath = @"C:\Github\UMUNA\UMUNA_Unity\builds\UMUNA.exe"
            };

            MainGrid.Children.Add(host);
        }
    }
}
