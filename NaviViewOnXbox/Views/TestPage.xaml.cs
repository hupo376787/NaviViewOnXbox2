using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace NaviViewOnXbox.Views
{
    public sealed partial class TestPage : Page, INotifyPropertyChanged
    {
        public TestPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            btn.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void btn_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Colors.Red);
        }

        private void btn_LosingFocus(Windows.UI.Xaml.UIElement sender, Windows.UI.Xaml.Input.LosingFocusEventArgs args)
        {
            (sender as Button).Background = new SolidColorBrush(Colors.Gray);
        }

        private void btn_PreviewKeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            switch (e.OriginalKey)
            {
                case VirtualKey.Left:
                case VirtualKey.GamepadDPadLeft:
                case VirtualKey.GamepadLeftThumbstickLeft:
                    (Application.Current as App).strNaviTag = "test";
                    ShellPage.shellPage.navigationView.IsPaneOpen = true;
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
