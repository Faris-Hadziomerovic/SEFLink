using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.ViewModels;
using System;
using System.Windows;

namespace SEFLink.UI
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        private IEventAggregator _eventAggregator;

        public MainWindow(MainViewModel ViewModel, IEventAggregator eventAggregator)
        {
            InitializeComponent();           

            DataContext = ViewModel;
            _viewModel = ViewModel;
            _eventAggregator = eventAggregator;            

            StateChanged += MainWindow_StateChanged;
            Loaded += MainWindow_Loaded;

            _eventAggregator.GetEvent<NewViewSelectedEvent>().Subscribe(OnNewViewSelected);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RegulateWindowState();
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            RegulateWindowState();
        }

        private void RegulateWindowState()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.BorderThickness = new System.Windows.Thickness(7);
            }
            else
            {
                this.BorderThickness = new System.Windows.Thickness(0);
            }

            _eventAggregator.GetEvent<WindowStateChangedEvent>().Publish(new WindowStateChangedEventArgs(this.WindowState));
        }

        private void OnNewViewSelected(NewViewSelectedEventArgs args)
        {
            MinWidth = args.MinWidth;

            if (args.MinHeight != -1)
            {
                MinHeight = args.MinHeight;
            }
        }

    }
}
