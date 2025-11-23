using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Umuna.Ui.Views
{
    public partial class ErrorDialog : Window
    {
        public ErrorDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string ErrorTitle { get; set; }
        public string ErrorMessage { get; set; }
        public string ExceptionDetails { get; set; }
        public bool ShowRetryButton { get; set; }

        private void OnCopyDetailsClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ExceptionDetails))
            {
                Clipboard.SetText(ExceptionDetails);
                MessageBox.Show("Error details copied to clipboard.", "Copied",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OnRetryClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OnDetailsToggle(object sender, RoutedEventArgs e)
        {
            DetailsExpander.IsExpanded = !DetailsExpander.IsExpanded;
        }
    }
}
