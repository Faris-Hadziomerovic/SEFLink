using Prism.Events;
using SEFLink.UI.Events;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SEFLink.UI.Views.TitleBar
{
    public partial class BaseTitleBarView : UserControl
    {        

        public BaseTitleBarView()
        {
            InitializeComponent();
        }

        public void ChangeResizeButtonIcon()
        {
            var window = Window.GetWindow(this);

            if (window.WindowState == WindowState.Maximized)
            {
                btnResize.Content = "🗗";
            }
            else
            {
                btnResize.Content = "🗖";
            }
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            window.WindowState = WindowState.Minimized;
        }

        private void Resize(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            window.Close();
        }
    }
}
