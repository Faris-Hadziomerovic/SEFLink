using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Constants;
using SEFLink.UI.HCI.Events;
using static SEFLink.UI.HCI.Constants.LanguageConstants;

namespace SEFLink.UI.HCI.ViewModels
{
    public class NavigationBarViewModel : Observable
    {
        #region Fields

        private string _foodText;
        private string _drinksText;

        private bool _drinksIsVisible;
        private bool _foodIsVisible;
        private bool _helpIsVisible;
        private bool _languagesIsVisible;
        private bool _englishFlagIsVisible;
        private bool _bosnianFlagIsVisible;
        private bool _germanFlagIsVisible;

        private readonly IEventAggregator _eventAggregator;

        public ICommand NavigateToFoodCommand { get; }
        public ICommand NavigateToDrinksCommand { get; }
        public ICommand NavigateToLanguagesCommand { get; }
        public ICommand NavigateToHelpCommand { get; }

        #endregion


        #region Constructor

        public NavigationBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Setup();

            NavigateToFoodCommand = new DelegateCommand(Execute_NavigateToFood, CanExecute_NavigateToFood);
            NavigateToDrinksCommand = new DelegateCommand(Execute_NavigateToDrinks, CanExecute_NavigateToDrinks);
            NavigateToLanguagesCommand = new DelegateCommand(Execute_NavigateToLanguages, CanExecute_NavigateToLanguages);
            NavigateToHelpCommand = new DelegateCommand(Execute_NavigateToHelp, CanExecute_NavigateToHelp);

            _eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);
        }

        #endregion


        #region Properties

        public string FoodText
        {
            get { return _foodText; }
            set { _foodText = value; OnPropertyChanged(); }
        }

        public string DrinksText
        {
            get { return _drinksText; }
            set { _drinksText = value; OnPropertyChanged(); }
        }

        public bool DrinksIsVisible
        {
            get { return _drinksIsVisible; }
            set { _drinksIsVisible = value; OnPropertyChanged(); }
        }

        public bool FoodIsVisible
        {
            get { return _foodIsVisible; }
            set { _foodIsVisible = value; OnPropertyChanged(); }
        }

        public bool HelpIsVisible
        {
            get { return _helpIsVisible; }
            set { _helpIsVisible = value; OnPropertyChanged(); }
        }

        public bool EnglishFlagIsVisible
        {
            get { return _englishFlagIsVisible; }
            set { _englishFlagIsVisible = value; OnPropertyChanged(); }
        }

        public bool BosnianFlagIsVisible
        {
            get { return _bosnianFlagIsVisible; }
            set { _bosnianFlagIsVisible = value; OnPropertyChanged(); }
        }

        public bool GermanFlagIsVisible
        {
            get { return _germanFlagIsVisible; }
            set { _germanFlagIsVisible = value; OnPropertyChanged(); }
        }

        public bool LanguagesIsVisible
        {
            get { return _languagesIsVisible; }
            set { _languagesIsVisible = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            DrinksIsVisible = true;
            FoodIsVisible = true;
            HelpIsVisible = true;
            LanguagesIsVisible = true;

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
            EnglishFlagIsVisible = true;
            BosnianFlagIsVisible = false;
            GermanFlagIsVisible = false;

            FoodText = English.Food;
            DrinksText = English.Drinks;
        }

        private void OnBosnianSelected()
        {
            EnglishFlagIsVisible = false;
            BosnianFlagIsVisible = true;
            GermanFlagIsVisible = false;

            FoodText = Bosnian.Food;
            DrinksText = Bosnian.Drinks;
        }

        private void OnGermanSelected()
        {
            EnglishFlagIsVisible = false;
            BosnianFlagIsVisible = false;
            GermanFlagIsVisible = true;

            FoodText = German.Food;
            DrinksText = German.Drinks;
        }

        private void Execute_NavigateToFood()
        {
            _eventAggregator.GetEvent<MenuViewEvent>().Publish(new MenuViewEventArgs());
            _eventAggregator.GetEvent<FoodViewEvent>().Publish(new FoodViewEventArgs());
        }

        private void Execute_NavigateToDrinks()
        {
            _eventAggregator.GetEvent<MenuViewEvent>().Publish(new MenuViewEventArgs());
            _eventAggregator.GetEvent<DrinksViewEvent>().Publish(new DrinksViewEventArgs());
        }

        private void Execute_NavigateToHelp()
        {
            _eventAggregator.GetEvent<MenuViewEvent>().Publish(new MenuViewEventArgs());
            //_eventAggregator.GetEvent<HelpViewEvent>().Publish(new HelpViewEventArgs());
            //_eventAggregator.GetEvent<PaymentOptionsViewEvent>().Publish(new PaymentOptionsViewEventArgs());
            _eventAggregator.GetEvent<AddItemEvent>().Publish(new AddItemEventArgs
            {
                OrderItem = new Models.OrderItem
                {
                    Image = "Sprite",
                    Name = "Sprite 0.5l",
                    Price = 1.99M,
                    Description = "Beef hamburger with ketchup."
                }
            });
        }

        private void Execute_NavigateToLanguages()
        {
            _eventAggregator.GetEvent<LanguageViewEvent>().Publish(new LanguageViewEventArgs());
        }

        private bool CanExecute_NavigateToFood()
        {
            return true;
        }

        private bool CanExecute_NavigateToDrinks()
        {
            return true;
        }

        private bool CanExecute_NavigateToHelp()
        {
            return true;
        }

        private bool CanExecute_NavigateToLanguages()
        {
            return true;
        }

        #endregion
    }
}
