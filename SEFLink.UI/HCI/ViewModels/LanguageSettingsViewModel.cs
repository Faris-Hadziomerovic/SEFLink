using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using System;
using System.Windows.Input;

namespace SEFLink.UI.HCI.ViewModels
{
    public class LanguageSettingsViewModel : Observable
    {
        #region Fields

        private string _infoText;

        private bool _englishIsVisible;
        private bool _bosnianIsVisible;
        private bool _germanIsVisible;

        private readonly IEventAggregator _eventAggregator;

        public ICommand SelectEnglishCommand { get; }
        public ICommand SelectBosnianCommand { get; }
        public ICommand SelectGermanCommand { get; }

        #endregion


        #region Constructor

        public LanguageSettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Setup();

            SelectEnglishCommand = new DelegateCommand(Execute_SelectEnglish, CanExecute_SelectEnglish);
            SelectBosnianCommand = new DelegateCommand(Execute_SelectBosnian, CanExecute_SelectBosnian);
            SelectGermanCommand = new DelegateCommand(Execute_SelectGerman, CanExecute_SelectGerman);

        }

        #endregion


        #region Properties

        public bool OriginIsPayment { get; set; }

        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; OnPropertyChanged(); }
        }

        public bool EnglishIsVisible
        {
            get { return _englishIsVisible; }
            set { _englishIsVisible = value; OnPropertyChanged(); }
        }

        public bool BosnianIsVisible
        {
            get { return _bosnianIsVisible; }
            set { _bosnianIsVisible = value; OnPropertyChanged(); }
        }

        public bool GermanIsVisible
        {
            get { return _germanIsVisible; }
            set { _germanIsVisible = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            InfoText = "";
            EnglishIsVisible = true;
            BosnianIsVisible = true;
            GermanIsVisible = true;
        }        
        
        private void Execute_SelectEnglish()
        {
            _eventAggregator.GetEvent<ChangeLanguageEvent>().Publish(new ChangeLanguageEventArgs { Language = "English" });            
            NavigateToMenuView();       
        }  

        private void Execute_SelectBosnian()
        {
            _eventAggregator.GetEvent<ChangeLanguageEvent>().Publish(new ChangeLanguageEventArgs { Language = "Bosnian" });            
            NavigateToMenuView();
        }

        private void Execute_SelectGerman()
        {
            _eventAggregator.GetEvent<ChangeLanguageEvent>().Publish(new ChangeLanguageEventArgs { Language = "German" });            
            NavigateToMenuView();
        }

        private void NavigateToMenuView()
        {
            _eventAggregator.GetEvent<MenuViewEvent>().Publish(new MenuViewEventArgs { OriginIsPaymentView = OriginIsPayment});
        }

        private bool CanExecute_SelectEnglish() => true;

        private bool CanExecute_SelectBosnian() => true;

        private bool CanExecute_SelectGerman() => true;

        #endregion
    }
}
