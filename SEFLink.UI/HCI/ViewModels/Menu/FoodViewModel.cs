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
    public class FoodViewModel : Observable
    {
        #region Fields

        private string _infoText;

        private string _foodText;

        private bool _foodIsVisible;

        private readonly IEventAggregator _eventAggregator;

        public ICommand NavigateToFoodCommand { get; }
        public ICommand NavigateBackCommand { get; }

        private OrderItemViewModel _hamburgerViewModel;
        private OrderItemViewModel _chickenViewModel;
        private OrderItemViewModel _friesViewModel;
        private OrderItemViewModel _pepperoniPizzaViewModel;
        private OrderItemViewModel _napolitanPizzaViewModel;
        private OrderItemViewModel _mexicanPizzaViewModel;

        #endregion


        #region Constructor

        public FoodViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NavigateToFoodCommand = new DelegateCommand(Execute_NavigateToFood, CanExecute_NavigateToFood);
            NavigateBackCommand = new DelegateCommand(Execute_NavigateBack, CanExecute_NavigateBack);

            Setup();

            _eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);
        }

        #endregion


        #region Properties

        public string FoodText
        {
            get { return _foodText; }
            set { _foodText = value; OnPropertyChanged(); }
        }

        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; OnPropertyChanged(); }
        }

        public bool FoodIsVisible
        {
            get { return _foodIsVisible; }
            set { _foodIsVisible = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel HamburgerViewModel
        {
            get { return _hamburgerViewModel; }
            set { _hamburgerViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel ChickenViewModel
        {
            get { return _chickenViewModel; }
            set { _chickenViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel FriesViewModel
        {
            get { return _friesViewModel; }
            set { _friesViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel NapolitanPizzaViewModel
        {
            get { return _napolitanPizzaViewModel; }
            set { _napolitanPizzaViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel PepperoniPizzaViewModel
        {
            get { return _pepperoniPizzaViewModel; }
            set { _pepperoniPizzaViewModel = value; OnPropertyChanged(); }
        }

        public OrderItemViewModel MexicanPizzaViewModel
        {
            get { return _mexicanPizzaViewModel; }
            set { _mexicanPizzaViewModel = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            InfoText = "";
            FoodIsVisible = false;

            HamburgerViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Food.Hamburger, false);
            ChickenViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Food.ChickenTenders, false);
            FriesViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Food.FrenchFries, false);
            PepperoniPizzaViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Food.PepperoniPizza, false);
            NapolitanPizzaViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Food.NapolitanPizza, false);
            MexicanPizzaViewModel = OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, DataProvider.Food.MexicanPizza, false);

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
            FoodText = English.Food;
        }

        private void OnBosnianSelected()
        {
            FoodText = Bosnian.Food;
        }

        private void OnGermanSelected()
        {
            FoodText = German.Food;
        }

        private void Execute_NavigateToFood()
        {
            _eventAggregator.GetEvent<FoodViewEvent>().Publish();
        }

        private void Execute_NavigateBack()
        {
            _eventAggregator.GetEvent<FoodDrinkViewEvent>().Publish();
        }

        private bool CanExecute_NavigateBack() => true;

        private bool CanExecute_NavigateToFood() => true;

        #endregion
    }
}