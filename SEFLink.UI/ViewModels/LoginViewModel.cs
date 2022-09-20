using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Data;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using System;
using System.Windows.Input;

namespace SEFLink.UI.ViewModels
{
    public class LoginViewModel : Observable
    {
        #region Fields

        private string _email;
        private string _password;
        private bool _rememberUser;
        private BaseTitleBarViewModel _baseTitleBarViewModel;
        private IUserDataService _userDataService;
        private IEventAggregator _eventAggregator;
        public ICommand SignInCommand { get; }

        #endregion


        #region Constructor

        public LoginViewModel(IUserDataService userDataService,
                              BaseTitleBarViewModel baseTitleBarViewModel,
                              IEventAggregator eventAggregator)
        {
            _userDataService = userDataService;
            _eventAggregator = eventAggregator;

            BaseTitleBarViewModel = baseTitleBarViewModel;

            SignInCommand = new DelegateCommand(Execute_SignIn, Can_SignIn);

            Email = "Admin_1@sefLink.com";
            Password = "Admin123";
            RememberUser = true;
        }

        #endregion


        #region Properties

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                ((DelegateCommand)SignInCommand).RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
                ((DelegateCommand)SignInCommand).RaiseCanExecuteChanged();
            }
        }

        public bool RememberUser
        {
            get { return _rememberUser; }
            set { _rememberUser = value; OnPropertyChanged(); }
        }

        public BaseTitleBarViewModel BaseTitleBarViewModel
        {
            get { return _baseTitleBarViewModel; }
            set { _baseTitleBarViewModel = value; OnPropertyChanged(); }
        }

        #endregion


        #region Other Methods

        private async void Execute_SignIn()
        {            
            var user = await _userDataService.GetUserAsync(_email, _password);

            if (user == null)
            {
                System.Windows.MessageBox.Show("Failed to log-in! No user with those credentials exists.");

                return;
            }
            
            _eventAggregator.GetEvent<LoggedInEvent>().Publish( new LoggedInEventArgs { Id = user.Id, UserName = user.Username, UserSettings = user.UserSettings } );

            if (!RememberUser)
            {
                Email = "";
                Password = "";
            }

        }

        private bool Can_SignIn()
        {
            return Password.Length != 0 && Email.Length != 0;
        }

        #endregion
    }
}
