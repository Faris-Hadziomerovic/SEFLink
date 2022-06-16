using Autofac;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.Startup;
using System;
using System.Windows;

namespace SEFLink.UI.ViewModels
{
    public class MainViewModel : Observable
    {
        #region Fields

        private BaseTitleBarViewModel _baseTitleBarViewModel;

        private LoginViewModel _loginViewModel;
        private DashboardViewModel _dashboardViewModel;

        private SettingsViewModel _settingsViewModel;

        private FirstSettingsViewModel _firstSettingsViewModel;
        private SecondSettingsViewModel _secondSettingsViewModel;

        private object _currentViewModel;
        private IEventAggregator _eventAggregator;

        #endregion


        #region Constructor

        public MainViewModel(BaseTitleBarViewModel baseTitleBarViewModel,
                             LoginViewModel loginViewModel,
                             DashboardViewModel dashboardViewModel,
                             SettingsViewModel settingsViewModel,
                             FirstSettingsViewModel firstSettingsViewModel,
                             SecondSettingsViewModel secondSettingsViewModel,
                             IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;

            BaseTitleBarViewModel = baseTitleBarViewModel;

            LoginViewModel = loginViewModel;
            DashboardViewModel = dashboardViewModel;

            SettingsViewModel = settingsViewModel;

            FirstSettingsViewModel = firstSettingsViewModel;
            SecondSettingsViewModel = secondSettingsViewModel;

            CurrentViewModel = loginViewModel;
            //CurrentViewModel = dashboardViewModel;


            _eventAggregator.GetEvent<LoggedInEvent>().Subscribe(OnLoggedIn);
            _eventAggregator.GetEvent<LoggedOutEvent>().Subscribe(OnLoggedOut);
            _eventAggregator.GetEvent<FinishSettingsViewEvent>().Subscribe(OnFinishedSettings);
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

        public FirstSettingsViewModel FirstSettingsViewModel
        {
            get { return _firstSettingsViewModel; }
            set { _firstSettingsViewModel = value; OnPropertyChanged(); }
        }

        public SecondSettingsViewModel SecondSettingsViewModel
        {
            get { return _secondSettingsViewModel; }
            set { _secondSettingsViewModel = value; OnPropertyChanged(); }
        }

        public DashboardViewModel DashboardViewModel
        {
            get { return _dashboardViewModel; }
            set { _dashboardViewModel = value; OnPropertyChanged(); }
        }

        #endregion



        #region Other Methods

        private void OnLoggedOut()
        {
            //_eventAggregator.GetEvent<NewViewSelectedEvent>().Publish(new NewViewSelectedEventArgs(500));

            CurrentViewModel = LoginViewModel;
        }

        private void OnLoggedIn(LoggedInEventArgs args)
        {
            //_eventAggregator.GetEvent<NewViewSelectedEvent>().Publish(new NewViewSelectedEventArgs(500));
  
            CurrentViewModel = SettingsViewModel;

            //LoginMessageTest(args);
        }

        private void OnFinishedSettings(FinishSettingsViewEventArgs settings)
        {
            //_eventAggregator.GetEvent<NewViewSelectedEvent>().Publish(new NewViewSelectedEventArgs(850));

            CurrentViewModel = DashboardViewModel;
        }

        private static void LoginMessageTest(LoggedInEventArgs args)
        {
            string WelcomeMessage = "Welcome";

            switch (args.IsAdmin)
            {
                case true:
                    WelcomeMessage += $" admin, {args.UserName}.";
                    break;
                default:
                    WelcomeMessage += $", {args.UserName}.";
                    break;
            }

            switch (args.RememberUser)
            {
                case true:
                    WelcomeMessage += $"\nYou will be remembered.";
                    break;
                default:
                    WelcomeMessage += $"\nThis is a one-time session.";
                    break;
            }


            System.Windows.MessageBox.Show(WelcomeMessage, "Log-in successful!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion


    }
}


// user login info

// user_1@gmail.com    qwe123
// user_2@gmail.com    qwe123
// user_3@gmail.com    qwe123

// Admin_1@sefLink.com Admin123
// Admin_2@sefLink.com Admin321