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

        private bool _drinksIsVisible;
        private bool _foodIsVisible;
        private bool _helpIsVisible;
        private bool _languagesIsVisible;

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

            DrinksIsVisible = true;
            FoodIsVisible = true;
            HelpIsVisible = true;
            LanguagesIsVisible = true;

            NavigateToFoodCommand = new DelegateCommand(Execute_NavigateToFood, CanExecute_NavigateToFood);
            NavigateToDrinksCommand = new DelegateCommand(Execute_NavigateToDrinks, CanExecute_NavigateToDrinks);
            NavigateToLanguagesCommand = new DelegateCommand(Execute_NavigateToLanguages, CanExecute_NavigateToLanguages);
            NavigateToHelpCommand = new DelegateCommand(Execute_NavigateToHelp, CanExecute_NavigateToHelp);
        }

        #endregion


        #region Properties

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

        public bool LanguagesIsVisible
        {
            get { return _languagesIsVisible; }
            set { _languagesIsVisible = value; OnPropertyChanged(); }
        }

        #endregion


        #region Other Methods

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

        private void Execute_NavigateToLanguages()
        {
            _eventAggregator.GetEvent<LanguageViewEvent>().Publish(new LanguageViewEventArgs());
        }

        private bool CanExecute_NavigateToLanguages()
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

        #endregion

    }
}
