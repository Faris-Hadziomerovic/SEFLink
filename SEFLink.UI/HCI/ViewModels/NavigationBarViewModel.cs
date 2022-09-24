using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;

namespace SEFLink.UI.HCI.ViewModels
{
    public class NavigationBarViewModel : Observable
    {
        #region Fields

        private string _englishFood = "Food";
        private string _bosnianFood = "Hrana";
        private string _germanFood = "Essen";
        
        private string _englishDrinks = "Drinks";
        private string _bosnianDrinks = "Pića";
        private string _germanDrinks = "Getränke";

        private string _foodText;
        private string _drinksText;

        private bool _drinksIsVisible;
        private bool _foodIsVisible;
        private bool _helpIsVisible;
        private bool _languagesIsVisible;
        private bool _englishFlagIsVisible;
        private bool _bosnianFlagIsVisible;
        private bool _germanFlagIsVisible;

        private IEventAggregator _eventAggregator;

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

            _eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageSelected);
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


        #region Other Methods

        private void Setup()
        {
            DrinksIsVisible = true;
            FoodIsVisible = true;
            HelpIsVisible = true;
            LanguagesIsVisible = true;

            OnEnglishSelected();
        }

        private void OnLanguageSelected(ChangeLanguageEventArgs args)
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

            FoodText = _englishFood;
            DrinksText = _englishDrinks;
        }
        
        private void OnBosnianSelected()
        {
            EnglishFlagIsVisible = false;
            BosnianFlagIsVisible = true;
            GermanFlagIsVisible = false;

            FoodText = _bosnianFood;
            DrinksText = _bosnianDrinks;
        }

        private void OnGermanSelected()
        {
            EnglishFlagIsVisible = false;
            BosnianFlagIsVisible = false;
            GermanFlagIsVisible = true;

            FoodText = _germanFood;
            DrinksText = _germanDrinks;
        }

        private void Execute_NavigateToFood()
        {
            _eventAggregator.GetEvent<MenuViewEvent>().Publish(new MenuViewEventArgs());
            _eventAggregator.GetEvent<FoodViewEvent>().Publish(new FoodViewEventArgs());
        }

        private bool CanExecute_NavigateToFood()
        {
            return true;
        }

        private void Execute_NavigateToDrinks()
        {
            _eventAggregator.GetEvent<MenuViewEvent>().Publish(new MenuViewEventArgs());
            _eventAggregator.GetEvent<DrinksViewEvent>().Publish(new DrinksViewEventArgs());
        }

        private bool CanExecute_NavigateToDrinks()
        {
            return true;
        }

        private void Execute_NavigateToHelp()
        {
            _eventAggregator.GetEvent<MenuViewEvent>().Publish(new MenuViewEventArgs());
            _eventAggregator.GetEvent<HelpViewEvent>().Publish(new HelpViewEventArgs());
        }

        private bool CanExecute_NavigateToHelp()
        {
            return true;
        }

        private void Execute_NavigateToLanguages()
        {
            _eventAggregator.GetEvent<LanguageViewEvent>().Publish(new LanguageViewEventArgs());
        }

        private bool CanExecute_NavigateToLanguages()
        {
            return true;
        }

        #endregion

    }
}
