using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Data;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.ViewModels.Payment;
using SEFLink.UI.HCI.Views.Payment;
using System;
using System.Collections.Generic;

namespace SEFLink.UI.HCI.ViewModels
{
    public class AppViewModel : Observable
    {
        #region Fields

        private NavigationBarViewModel _navigationBarViewModel;
        private DialogOverlayViewModel _dialogOverlayViewModel;

        private MenuViewModel _menuViewModel;
        private LanguageSettingsViewModel _languageSettingsViewModel;

        private object _currentViewModel;
        private readonly IEventAggregator _eventAggregator;

        private bool _dialogOverlayIsVisible;

        // for testing views
        private string _viewTitle;
        private readonly string _menuTitle = "Menu";
        private readonly string _languageTitle = "Language";

        #endregion


        #region Constructor

        public AppViewModel(NavigationBarViewModel navigationBarViewModel,
                            DialogOverlayViewModel dialogOverlayViewModel,
                            MenuViewModel menuViewModel,
                            LanguageSettingsViewModel languageSettingsViewModel,
                            IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            MenuViewModel = menuViewModel;
            NavigationBarViewModel = navigationBarViewModel;
            DialogOverlayViewModel = dialogOverlayViewModel;
            LanguageSettingsViewModel = languageSettingsViewModel;

            Setup();

            _eventAggregator.GetEvent<MenuViewEvent>().Subscribe(OnMenuViewSelected);
            _eventAggregator.GetEvent<LanguageViewEvent>().Subscribe(OnLanguageViewSelected);

            _eventAggregator.GetEvent<CloseDialogOverlayEvent>().Subscribe(OnOverlayClosed);
            _eventAggregator.GetEvent<OpenDialogOverlayEvent>().Subscribe(OnOverlayOpened);
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

        public DialogOverlayViewModel DialogOverlayViewModel
        {
            get { return _dialogOverlayViewModel; }
            set { _dialogOverlayViewModel = value; OnPropertyChanged(); }
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

        public bool DialogOverlayIsVisible
        {
            get { return _dialogOverlayIsVisible; }
            set { _dialogOverlayIsVisible = value; OnPropertyChanged(); }
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

            DialogOverlayIsVisible = false;
        }

        private void OnMenuViewSelected(MenuViewEventArgs args)
        {
            CurrentViewModel = MenuViewModel;
            ViewTitle = _menuTitle;
        }

        private void OnLanguageViewSelected(LanguageViewEventArgs args)
        {
            CurrentViewModel = LanguageSettingsViewModel;
            LanguageSettingsViewModel.OriginIsPayment = IsOfPaymentViewModelType(MenuViewModel.CurrentViewModel);
            ViewTitle = _languageTitle;
        }

        private bool IsOfPaymentViewModelType(object currentViewModel)
        {
            var viewModelType = currentViewModel.GetType();

            List<Type> paymentViewModelTypes = new List<Type>
            {
                typeof(PaymentOptionsViewModel),
                typeof(PaymentCreditCardViewModel),
                typeof(PaymentCashViewModel)
            };

            return paymentViewModelTypes.Contains(viewModelType);
        }

        private void OnOverlayClosed()
        {
            DialogOverlayIsVisible = false;
        }

        private void OnOverlayOpened()
        {
            DialogOverlayIsVisible = true;
        }

        #endregion
    }
}
