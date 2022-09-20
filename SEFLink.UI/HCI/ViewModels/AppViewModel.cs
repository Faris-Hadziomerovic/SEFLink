using Prism.Commands;
using Prism.Events;
using SEFLink.Model;
using SEFLink.UI.Data;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using System;
using System.Windows;
using System.Windows.Input;
//using SEFLink.UI.ViewModels;

namespace SEFLink.UI.HCI.ViewModels
{
    public class AppViewModel : Observable
    {
        #region Fields

        private User _user;

        private NavigationBarViewModel _navigationBarViewModel;

        private MainViewModel _mainViewModel;
        private LanguageSettingsViewModel _languageSettingsViewModel;

        private object _currentSettingsViewModel;
        private IEventAggregator _eventAggregator;

        private string _viewTitle;
        private readonly string _firstTitle = "Main";
        private readonly string _secondTitle = "Language";

        #endregion


        #region Constructor

        public AppViewModel(NavigationBarViewModel navigationBarViewModel,
                            MainViewModel mainViewModel,
                            LanguageSettingsViewModel languageSettingsViewModel,
                            IEventAggregator eventAggregator)
        {

            NavigationBarViewModel = navigationBarViewModel;

            MainViewModel = mainViewModel;
            LanguageSettingsViewModel = languageSettingsViewModel;

            CurrentSettingsViewModel = mainViewModel;

            ViewTitle = _firstTitle;

            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<LoggedInEvent>().Subscribe(OnLoggedIn);
            _eventAggregator.GetEvent<MainViewEvent>().Subscribe(OnMainView);
            _eventAggregator.GetEvent<LanguageViewEvent>().Subscribe(OnLanguageView);
            _eventAggregator.GetEvent<CancelOrderEvent>().Subscribe(OnCancelOrder);
        }

        #endregion


        #region Properties

        public User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        public object CurrentSettingsViewModel
        {
            get { return _currentSettingsViewModel; }
            set { _currentSettingsViewModel = value; OnPropertyChanged(); }
        }

        public NavigationBarViewModel NavigationBarViewModel
        {
            get { return _navigationBarViewModel; }
            set { _navigationBarViewModel = value; OnPropertyChanged(); }
        }

        public MainViewModel MainViewModel
        {
            get { return _mainViewModel; }
            set { _mainViewModel = value; OnPropertyChanged(); }
        }

        public LanguageSettingsViewModel LanguageSettingsViewModel
        {
            get { return _languageSettingsViewModel; }
            set { _languageSettingsViewModel = value; OnPropertyChanged(); }
        }

        public string ViewTitle
        {
            get { return _viewTitle; }
            set { _viewTitle = value; OnPropertyChanged(); }
        }

        #endregion


        #region Other Methods

        private void OnLoggedIn(LoggedInEventArgs args)
        {
            CurrentSettingsViewModel = MainViewModel;
        }

        private void OnMainView(MainViewEventArgs args)
        {
            CurrentSettingsViewModel = LanguageSettingsViewModel;
            ViewTitle = _secondTitle;
        }

        private void OnLanguageView(LanguageViewEventArgs args)
        {
            CurrentSettingsViewModel = MainViewModel;
            ViewTitle = _firstTitle;
        }

        private void OnCancelOrder(CancelOrderEventArgs args)
        {
        }

        #endregion
    }
}
