using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace SEFLink.UI.ViewModels
{
    public class MainViewModel : Observable
    {
        #region Fields

        private object _currentViewModel;

        private BaseTitleBarViewModel _baseTitleBarViewModel;

        private LoginViewModel _loginViewModel;
        private SettingsViewModel _settingsViewModel;
        private DashboardViewModel _dashboardViewModel;
        private AppViewModel _appViewModel;

        private IEventAggregator _eventAggregator;

        #endregion


        #region Constructor

        public MainViewModel(BaseTitleBarViewModel baseTitleBarViewModel,
                             LoginViewModel loginViewModel,
                             DashboardViewModel dashboardViewModel,
                             SettingsViewModel settingsViewModel,
                             AppViewModel appViewModel,
                             IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;

            BaseTitleBarViewModel = baseTitleBarViewModel;

            LoginViewModel = loginViewModel;
            SettingsViewModel = settingsViewModel;
            DashboardViewModel = dashboardViewModel;
            
            _appViewModel = appViewModel;
            CurrentViewModel = appViewModel;

            _eventAggregator.GetEvent<LoggedInEvent>().Subscribe(OnLoggedIn);
            _eventAggregator.GetEvent<LoggedOutEvent>().Subscribe(OnLoggedOut);
            _eventAggregator.GetEvent<FinishSettingsViewEvent>().Subscribe(OnFinishedSettings);
            _eventAggregator.GetEvent<AppViewEvent>().Subscribe(OnSwitchToMyOrderApp);
        }

        #endregion


        #region Properties

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (value is LoginViewModel)
                    _eventAggregator.GetEvent<NewViewSelectedEvent>().Publish(new NewViewSelectedEventArgs(500));

                if (value is SettingsViewModel)
                    _eventAggregator.GetEvent<NewViewSelectedEvent>().Publish(new NewViewSelectedEventArgs(500));

                if (value is DashboardViewModel)
                    _eventAggregator.GetEvent<NewViewSelectedEvent>().Publish(new NewViewSelectedEventArgs(1000));
                
                if (value is AppViewModel)
                    _eventAggregator.GetEvent<NewViewSelectedEvent>().Publish(new NewViewSelectedEventArgs(1000));


                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public BaseTitleBarViewModel BaseTitleBarViewModel
        {
            get { return _baseTitleBarViewModel; }
            set { _baseTitleBarViewModel = value; OnPropertyChanged(); }
        }

        public LoginViewModel LoginViewModel
        {
            get { return _loginViewModel; }
            set { _loginViewModel = value; OnPropertyChanged(); }
        }

        public SettingsViewModel SettingsViewModel
        {
            get { return _settingsViewModel; }
            set { _settingsViewModel = value; OnPropertyChanged(); }
        }        

        public DashboardViewModel DashboardViewModel
        {
            get { return _dashboardViewModel; }
            set { _dashboardViewModel = value; OnPropertyChanged(); }
        }

        #endregion


        #region Other Methods

        private async void OnLoggedOut()
        {
            await Task.Delay(300); // simulating logging out

            CurrentViewModel = LoginViewModel;
        }

        private void OnLoggedIn(LoggedInEventArgs args)
        {
            CurrentViewModel = SettingsViewModel;
        }

        private void OnFinishedSettings(FinishSettingsViewEventArgs settings)
        {
            CurrentViewModel = DashboardViewModel;
        }
        
        private void OnSwitchToMyOrderApp(AppViewEventArgs args)
        {
            CurrentViewModel = _appViewModel;
        }

        #endregion


    }
}


// user login info

// user_1@gmail.com         qwe123
// user_2@gmail.com         qwe123
// user_3@gmail.com         qwe123

// Admin_1@sefLink.com      Admin123
// Admin_2@sefLink.com      Admin321