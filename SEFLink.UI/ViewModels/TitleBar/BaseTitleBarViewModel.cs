using Prism.Events;
using SEFLink.UI.Events;

namespace SEFLink.UI.ViewModels
{
    public class BaseTitleBarViewModel : Observable
    {
        private IEventAggregator _eventAggregator;

        private string _resizeIcon;

        public string ResizeIcon
        {
            get { return _resizeIcon; }
            set { _resizeIcon = value; OnPropertyChanged(); }
        }

        public BaseTitleBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<WindowStateChangedEvent>().Subscribe(OnWindowStateChangedEvent);

        }

        private void OnWindowStateChangedEvent(WindowStateChangedEventArgs args)
        {
            if (args.NewState == System.Windows.WindowState.Normal)
            {
                ResizeIcon = "🗖";
            }
            if (args.NewState == System.Windows.WindowState.Maximized)
            {
                ResizeIcon = "🗗";
            }

        }
    }
}
