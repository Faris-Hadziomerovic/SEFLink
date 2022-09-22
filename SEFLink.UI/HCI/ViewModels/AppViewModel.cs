using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;

namespace SEFLink.UI.HCI.ViewModels
{
    public class AppViewModel : Observable
    {
        #region Fields

        private NavigationBarViewModel _navigationBarViewModel;

        private MenuViewModel _menuViewModel;
        private LanguageSettingsViewModel _languageSettingsViewModel;

        private object _currentViewModel;
        private IEventAggregator _eventAggregator;

        private string _viewTitle;
        private readonly string _menuTitle = "Menu";
        private readonly string _languageTitle = "Language";

        #endregion


        #region Constructor

        public AppViewModel(NavigationBarViewModel navigationBarViewModel,
                            MenuViewModel menuViewModel,
                            LanguageSettingsViewModel languageSettingsViewModel,
                            IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            NavigationBarViewModel = navigationBarViewModel;
            MenuViewModel = menuViewModel;
            LanguageSettingsViewModel = languageSettingsViewModel;

            Setup();

            _eventAggregator.GetEvent<MenuViewEvent>().Subscribe(OnMenuViewSelected);
            _eventAggregator.GetEvent<LanguageViewEvent>().Subscribe(OnLanguageViewSelected);
            _eventAggregator.GetEvent<CancelOrderEvent>().Subscribe(OnCancelOrder);
        }

        #endregion


        #region Properties

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public NavigationBarViewModel NavigationBarViewModel
        {
            get { return _navigationBarViewModel; }
            set { _navigationBarViewModel = value; OnPropertyChanged(); }
        }

        public MenuViewModel MenuViewModel
        {
            get { return _menuViewModel; }
            set { _menuViewModel = value; OnPropertyChanged(); }
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

        private void Setup()
        {
            CurrentViewModel = MenuViewModel;

            ViewTitle = _menuTitle;
        }

        private void OnMenuViewSelected(MenuViewEventArgs args)
        {
            CurrentViewModel = MenuViewModel;
            ViewTitle = _menuTitle;
        }

        private void OnLanguageViewSelected(LanguageViewEventArgs args)
        {
            CurrentViewModel = LanguageSettingsViewModel;
            ViewTitle = _languageTitle;
        }

        private void OnCancelOrder(CancelOrderEventArgs args)
        {
        }

        #endregion
    }
}
