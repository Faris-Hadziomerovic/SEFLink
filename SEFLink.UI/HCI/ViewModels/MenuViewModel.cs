﻿using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.Events.OtherEvents;
using SEFLink.UI.HCI.ViewModels.Menu;
using SEFLink.UI.HCI.ViewModels.Payment;

namespace SEFLink.UI.HCI.ViewModels
{
    public class MenuViewModel : Observable
    {
        #region Fields

        private SidebarViewModel _sidebarViewModel;

        private PaymentOptionsViewModel _paymentOptionsViewModel;
        private PaymentCashViewModel _paymentCashViewModel;
        private PaymentCreditCardViewModel _paymentCreditCardViewModel;

        private FoodDrinkViewModel _foodDrinkViewModel;
        private FoodViewModel _foodViewModel;
        private DrinksViewModel _drinksViewModel;

        private object _currentViewModel;

        private IEventAggregator _eventAggregator;

        #endregion


        #region Constructor

        public MenuViewModel(SidebarViewModel sidebarViewModel,
                             PaymentOptionsViewModel paymentOptionsViewModel,
                             PaymentCashViewModel paymentCashViewModel,
                             PaymentCreditCardViewModel paymentCreditCardViewModel,
                             FoodDrinkViewModel foodDrinkViewModel,
                             FoodViewModel foodViewModel,
                             DrinksViewModel drinksViewModel,
                             IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            SidebarViewModel = sidebarViewModel;

            FoodViewModel = foodViewModel;
            FoodDrinkViewModel = foodDrinkViewModel;
            DrinksViewModel = drinksViewModel;

            PaymentOptionsViewModel = paymentOptionsViewModel;
            PaymentCashViewModel = paymentCashViewModel;
            PaymentCreditCardViewModel = paymentCreditCardViewModel;

            Setup();

            _eventAggregator.GetEvent<FoodDrinkViewEvent>().Subscribe(OnStartViewSelected);
            _eventAggregator.GetEvent<FoodViewEvent>().Subscribe(OnFoodViewSelected);
            _eventAggregator.GetEvent<DrinksViewEvent>().Subscribe(OnDrinksViewSelected);

            _eventAggregator.GetEvent<PaymentOptionsViewEvent>().Subscribe(OnPaymentOptionsViewSelected);
            _eventAggregator.GetEvent<PaymentCashViewEvent>().Subscribe(OnPaymentCashViewSelected);
            _eventAggregator.GetEvent<PaymentCreditCardViewEvent>().Subscribe(OnPaymentCreditCardViewSelected);
            
            _eventAggregator.GetEvent<CheckoutCanceledEvent>().Subscribe(OnStartViewSelected);
        }

        #endregion


        #region Properties

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public SidebarViewModel SidebarViewModel
        {
            get { return _sidebarViewModel; }
            set { _sidebarViewModel = value; OnPropertyChanged(); }
        }

        public FoodDrinkViewModel FoodDrinkViewModel
        {
            get { return _foodDrinkViewModel; }
            set { _foodDrinkViewModel = value; OnPropertyChanged(); }
        }

        public FoodViewModel FoodViewModel
        {
            get { return _foodViewModel; }
            set { _foodViewModel = value; OnPropertyChanged(); }
        }

        public DrinksViewModel DrinksViewModel
        {
            get { return _drinksViewModel; }
            set { _drinksViewModel = value; OnPropertyChanged(); }
        }

        public PaymentOptionsViewModel PaymentOptionsViewModel
        {
            get { return _paymentOptionsViewModel; }
            set { _paymentOptionsViewModel = value; OnPropertyChanged(); }
        }

        public PaymentCashViewModel PaymentCashViewModel
        {
            get { return _paymentCashViewModel; }
            set { _paymentCashViewModel = value; OnPropertyChanged(); }
        }

        public PaymentCreditCardViewModel PaymentCreditCardViewModel
        {
            get { return _paymentCreditCardViewModel; }
            set { _paymentCreditCardViewModel = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            OnStartViewSelected();
        }

        private void OnStartViewSelected()
        {
            CurrentViewModel = FoodDrinkViewModel;
        }

        private void OnDrinksViewSelected()
        {
            CurrentViewModel = DrinksViewModel;
        }

        private void OnFoodViewSelected()
        {
            CurrentViewModel = FoodViewModel;
        }

        private void OnPaymentOptionsViewSelected()
        {
            CurrentViewModel = PaymentOptionsViewModel;
        }
        
        private void OnPaymentCashViewSelected()
        {
            CurrentViewModel = PaymentCashViewModel;
        }

        private void OnPaymentCreditCardViewSelected()
        {
            CurrentViewModel = PaymentCreditCardViewModel;
        }

        #endregion
    }
}
