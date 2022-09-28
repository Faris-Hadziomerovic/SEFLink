using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using System.Windows.Input;
using static SEFLink.UI.HCI.Constants.LanguageConstants;

namespace SEFLink.UI.HCI.ViewModels.Payment
{
    public class PaymentCreditCardViewModel : Observable
    {
        #region Fields

        private string _infoText;

        private string _cashText;
        private string _creditCardText;

        private bool _cashIsVisible;
        private bool _creditCardIsVisible;

        private readonly IEventAggregator _eventAggregator;

        public ICommand NavigateToCashCommand { get; }
        public ICommand NavigateToCreditCardCommand { get; }
        public ICommand NavigateBackCommand { get; }

        
        #endregion


        #region Constructor

        public PaymentCreditCardViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Setup();

            NavigateToCashCommand = new DelegateCommand(Execute_NavigateToCash, CanExecute_NavigateToCash);
            NavigateToCreditCardCommand = new DelegateCommand(Execute_NavigateToCreditCard, CanExecute_NavigateToCreditCard);
            NavigateBackCommand = new DelegateCommand(Execute_NavigateBack, CanExecute_NavigateBack);

            _eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);
        }

        #endregion


        #region Properties

        public string CashText
        {
            get { return _cashText; }
            set { _cashText = value; OnPropertyChanged(); }
        }

        public string CreditCardText
        {
            get { return _creditCardText; }
            set { _creditCardText = value; OnPropertyChanged(); }
        }

        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; OnPropertyChanged(); }
        }

        public bool CashIsVisible
        {
            get { return _cashIsVisible; }
            set { _cashIsVisible = value; OnPropertyChanged(); }
        }

        public bool CreditCardIsVisible
        {
            get { return _creditCardIsVisible; }
            set { _creditCardIsVisible = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            InfoText = "";
            CashIsVisible = true;
            CreditCardIsVisible = true;

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
            CreditCardText = English.CreditCard;
        }

        private void OnBosnianSelected()
        {
            CashText = Bosnian.Cash;
            CreditCardText = Bosnian.CreditCard;
        }

        private void OnGermanSelected()
        {
            CashText = German.Cash;
            CreditCardText = German.CreditCard;
        }

        private void Execute_NavigateToCash()
        {
            _eventAggregator.GetEvent<PaymentCashViewEvent>().Publish();
        }

        private void Execute_NavigateToCreditCard()
        {
            _eventAggregator.GetEvent<PaymentCreditCardViewEvent>().Publish();
        }
        
        private void Execute_NavigateBack()
        {
            _eventAggregator.GetEvent<PaymentOptionsViewEvent>().Publish();
        }
        
        private bool CanExecute_NavigateBack() => true;

        private bool CanExecute_NavigateToCash() => true;

        private bool CanExecute_NavigateToCreditCard() => true;

        #endregion
    }
}