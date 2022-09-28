using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
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
        private string _drinksText;

        private bool _foodIsVisible;
        private bool _drinksIsVisible;

        private readonly IEventAggregator _eventAggregator;

        public ICommand NavigateToFoodCommand { get; }
        public ICommand NavigateToDrinksCommand { get; }

        #endregion


        #region Constructor

        public FoodViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Setup();

            NavigateToFoodCommand = new DelegateCommand(Execute_NavigateToFood, CanExecute_NavigateToFood);
            NavigateToDrinksCommand = new DelegateCommand(Execute_NavigateToDrinks, CanExecute_NavigateToDrinks);

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

        public bool DrinksIsVisible
        {
            get { return _drinksIsVisible; }
            set { _drinksIsVisible = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            InfoText = "";
            FoodIsVisible = true;
            DrinksIsVisible = true;

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
            DrinksText = English.Drinks;
        }

        private void OnBosnianSelected()
        {
            FoodText = Bosnian.Food;
            DrinksText = Bosnian.Drinks;
        }

        private void OnGermanSelected()
        {
            FoodText = German.Food;
            DrinksText = German.Drinks;
        }

        private void Execute_NavigateToFood()
        {
            _eventAggregator.GetEvent<FoodViewEvent>().Publish();
        }

        private void Execute_NavigateToDrinks()
        {
            _eventAggregator.GetEvent<DrinksViewEvent>().Publish();
        }

        private bool CanExecute_NavigateToFood() => true;

        private bool CanExecute_NavigateToDrinks() => true;

        #endregion
    }
}