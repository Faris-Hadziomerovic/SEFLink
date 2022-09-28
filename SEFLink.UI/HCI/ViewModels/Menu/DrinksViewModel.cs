using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Data;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.Helpers;
using System;
using System.Windows.Input;
using static SEFLink.UI.HCI.Constants.LanguageConstants;

namespace SEFLink.UI.HCI.ViewModels.Menu
{
    public class DrinksViewModel : Observable
    {
        #region Fields

        private string _infoText;

        private string _drinksText;

        private bool _drinksIsVisible;

        private readonly IEventAggregator _eventAggregator;

        public ICommand NavigateToDrinksCommand { get; }
        public ICommand NavigateBackCommand { get; }

        private OrderItemViewModel _colaViewModel;
        private OrderItemViewModel _spriteViewModel;
        private OrderItemViewModel _fantaViewModel;
        private OrderItemViewModel _cappuccinoViewModel;
        private OrderItemViewModel _hotChocolateViewModel;
        private OrderItemViewModel _cafeLatteViewModel;        

        #endregion


        #region Constructor

        public DrinksViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NavigateToDrinksCommand = new DelegateCommand(Execute_NavigateToDrinks, CanExecute_NavigateToDrinks);
            NavigateBackCommand = new DelegateCommand(Execute_NavigateBack, CanExecute_NavigateBack);

            Setup();

            _eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);
        }
      
        #endregion


        #region Properties

        public string DrinksText
        {
            get { return _drinksText; }
            set { _drinksText = value; OnPropertyChanged(); }
        }

        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; OnPropertyChanged(); }
        }

        public bool DrinksIsVisible
        {
            get { return _drinksIsVisible; }
            set { _drinksIsVisible = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel ColaViewModel
        {
            get { return _colaViewModel; }
            set { _colaViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel SpriteViewModel
        {
            get { return _spriteViewModel; }
            set { _spriteViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel FantaViewModel
        {
            get { return _fantaViewModel; }
            set { _fantaViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel HotChocolateViewModel
        {
            get { return _hotChocolateViewModel; }
            set { _hotChocolateViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel CappuccinoViewModel
        {
            get { return _cappuccinoViewModel; }
            set { _cappuccinoViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel CafeLatteViewModel
        {
            get { return _cafeLatteViewModel; }
            set { _cafeLatteViewModel = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            InfoText = "";
            DrinksIsVisible = false;

            ColaViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Drinks.Cola, false);
            SpriteViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Drinks.Sprite, false);
            FantaViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Drinks.Fanta, false);
            HotChocolateViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Drinks.HotChocolate, false);
            CappuccinoViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Drinks.Cappuccino, false);
            CafeLatteViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Drinks.CafeLatte, false);

            OnEnglishSelected();
        }

        private void OnLanguageChanged(ChangeLanguageEventArgs args)
        {
            if (args.Language == "English")
                OnEnglishSelected();

            if (args.Language == "Bosnian")
                OnBosnianSelected();

            if (args.Language == "German")
                OnGermanSelected();
        }

        private void OnEnglishSelected()
        {
            DrinksText = English.Drinks;
        }

        private void OnBosnianSelected()
        {
            DrinksText = Bosnian.Drinks;
        }

        private void OnGermanSelected()
        {
            DrinksText = German.Drinks;
        }

        private void Execute_NavigateToDrinks()
        {
            _eventAggregator.GetEvent<DrinksViewEvent>().Publish();
        }

        private void Execute_NavigateBack()
        {
            _eventAggregator.GetEvent<FoodDrinkViewEvent>().Publish();
        }

        private bool CanExecute_NavigateBack() => true;

        private bool CanExecute_NavigateToDrinks() => true;

        #endregion
    }
}