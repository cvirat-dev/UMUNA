using System.Windows;
using System.Windows.Controls;

namespace Umuna.Ui.Controls.Attached
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BoundPassword",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged)
            );

        public static readonly DependencyProperty BindPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BindPassword",
                typeof(bool),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(false, OnBindPasswordChanged)
            );

        private static readonly DependencyProperty UpdatingPasswordProperty =
            DependencyProperty.RegisterAttached(
                "UpdatingPassword",
                typeof(bool),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(false)
            );

        public static string GetBoundPassword(DependencyObject d)
            => (string)d.GetValue(BoundPasswordProperty);

        public static void SetBoundPassword(DependencyObject d, string value)
            => d.SetValue(BoundPasswordProperty, value);

        public static bool GetBindPassword(DependencyObject d)
            => (bool)d.GetValue(BindPasswordProperty);

        public static void SetBindPassword(DependencyObject d, bool value)
            => d.SetValue(BindPasswordProperty, value);

        private static bool GetUpdatingPassword(DependencyObject d)
            => (bool)d.GetValue(UpdatingPasswordProperty);

        private static void SetUpdatingPassword(DependencyObject d, bool value)
        => d.SetValue(UpdatingPasswordProperty, value);

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= HandlePasswordChanged;
                if (!GetUpdatingPassword(passwordBox))
                {
                    passwordBox.Password = e.NewValue?.ToString() ?? string.Empty;
                }
                passwordBox.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                bool wasBound = (bool)e.OldValue;
                bool needToBind = (bool)e.NewValue;

                if (wasBound)
                {
                    passwordBox.PasswordChanged -= HandlePasswordChanged;
                }
                if (needToBind)
                {
                    passwordBox.PasswordChanged += HandlePasswordChanged;
                }
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SetUpdatingPassword(passwordBox, true);
                SetBoundPassword(passwordBox, passwordBox.Password);
                SetUpdatingPassword(passwordBox, false);
            }
        }
    }
}
