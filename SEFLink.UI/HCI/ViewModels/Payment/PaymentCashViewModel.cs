using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using System.Windows.Input;
using static SEFLink.UI.HCI.Constants.LanguageConstants;

namespace SEFLink.UI.HCI.ViewModels.Payment
{
    public class PaymentCashViewModel : Observable
    {
        #region Fields

        private string _infoText;

        private string _cashText;

        private bool _cashIsVisible;

        private readonly IEventAggregator _eventAggregator;

        public ICommand NavigateToCashCommand { get; }
        public ICommand NavigateBackCommand { get; }        

        #endregion


        #region Constructor

        public PaymentCashViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NavigateToCashCommand = new DelegateCommand(Execute_NavigateToCash, CanExecute_NavigateToCash);
            NavigateBackCommand = new DelegateCommand(Execute_NavigateBack, CanExecute_NavigateBack);

            Setup();

            _eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);
        }

        #endregion


        #region Properties

        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; OnPropertyChanged(); }
        }

        public string CashText
        {
            get { return _cashText; }
            set { _cashText = value; OnPropertyChanged(); }
        }

        public bool CashIsVisible
        {
            get { return _cashIsVisible; }
            set { _cashIsVisible = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            InfoText = "";
            CashIsVisible = true;

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
            CashText = English.Cash;
        }

        private void OnBosnianSelected()
        {
            CashText = Bosnian.Cash;
        }

        private void OnGermanSelected()
        {
            CashText = German.Cash;
        }

        private void Execute_NavigateToCash()
        {
            _eventAggregator.GetEvent<PaymentCashViewEvent>().Publish();
        }

        private void Execute_NavigateBack()
        {
            _eventAggregator.GetEvent<PaymentOptionsViewEvent>().Publish();
        }

        private bool CanExecute_NavigateBack() => true;

        private bool CanExecute_NavigateToCash() => true;

        #endregion
    }
}