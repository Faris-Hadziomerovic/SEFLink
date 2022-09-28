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

        private string _creditCardText;

        private bool _creditCardIsVisible;

        private readonly IEventAggregator _eventAggregator;

        public ICommand NavigateToCreditCardCommand { get; }
        public ICommand NavigateBackCommand { get; }
        
        #endregion


        #region Constructor

        public PaymentCreditCardViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NavigateToCreditCardCommand = new DelegateCommand(Execute_NavigateToCreditCard, CanExecute_NavigateToCreditCard);
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

        public string CreditCardText
        {
            get { return _creditCardText; }
            set { _creditCardText = value; OnPropertyChanged(); }
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
            CreditCardText = English.CreditCard;
        }

        private void OnBosnianSelected()
        {
            CreditCardText = Bosnian.CreditCard;
        }

        private void OnGermanSelected()
        {
            CreditCardText = German.CreditCard;
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

        private bool CanExecute_NavigateToCreditCard() => true;

        #endregion
    }
}