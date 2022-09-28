using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.Models;
using System.Windows.Input;
using static SEFLink.UI.HCI.Constants.LanguageConstants;

namespace SEFLink.UI.HCI.ViewModels
{
    public class DialogOverlayViewModel : Observable
	{
		#region Fields

		private string _question;

		private string _removeQuestion;
		private string _confirmQuestion;
		private string _cancelQuestion;

		private string _cancelOrderButtonText;
		private string _removeButtonText;
		private string _confirmButtontext;

		private bool _confirmDialogIsVisible;
		private bool _removeDialogIsVisible;
		private bool _cancelOrderDialogIsVisible;

		private string _cancelButtonText;
		private string _alternateCancelButtonText;

		private readonly IEventAggregator _eventAggregator;

		public ICommand ConfirmCommand { get; }
		public ICommand CloseCommand { get; }
		public ICommand CancelOrderCommand { get; }
		public ICommand RemoveCommand { get; }

		private OrderItem _orderItem;

		#endregion


		#region Constructor

		public DialogOverlayViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;

			ConfirmCommand = new DelegateCommand(Execute_Confirm);
			CloseCommand = new DelegateCommand(Execute_Close);
			CancelOrderCommand = new DelegateCommand(Execute_CancelOrder);
			RemoveCommand = new DelegateCommand(Execute_Remove);

			_eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);

			_eventAggregator.GetEvent<RemoveItemEvent>().Subscribe(OnRemoveItem);
			_eventAggregator.GetEvent<CheckoutEvent>().Subscribe(OnCheckout);
			_eventAggregator.GetEvent<CancelOrderEvent>().Subscribe(OnCancelOrder);
			
			Setup();
		}

		#endregion


		#region Properties

		public string Question
		{
			get { return _question; }
			set { _question = value; OnPropertyChanged(); }
		}
	   
		public string ConfirmButtonText
		{
			get { return _confirmButtontext; }
			set { _confirmButtontext = value; OnPropertyChanged(); }
		}

		public string RemoveButtonText
		{
			get { return _removeButtonText; }
			set { _removeButtonText = value; OnPropertyChanged(); }
		}

		public string CancelOrderButtonText
		{
			get { return _cancelOrderButtonText; }
			set { _cancelOrderButtonText = value; OnPropertyChanged(); }
		}
		
		public string CancelButtonText
		{
			get { return _cancelButtonText; }
			set { _cancelButtonText = value; OnPropertyChanged(); }
		}

		public string AlternateCancelButtonText
		{
			get { return _alternateCancelButtonText; }
			set { _alternateCancelButtonText = value; OnPropertyChanged(); }
		}

		public bool ConfirmDialogIsVisible
		{
			get { return _confirmDialogIsVisible; }
			set { _confirmDialogIsVisible = value; OnPropertyChanged(); }
		}

		public bool RemoveDialogIsVisible
		{
			get { return _removeDialogIsVisible; }
			set { _removeDialogIsVisible = value; OnPropertyChanged(); }
		}
		
		public bool CancelOrderDialogIsVisible
		{
			get { return _cancelOrderDialogIsVisible; }
			set { _cancelOrderDialogIsVisible = value; OnPropertyChanged(); }
		}
		
		#endregion


		#region Methods

		private void Setup()
		{
			OnEnglishSelected();

			Question = _confirmQuestion;

			ConfirmDialogIsVisible = true;
			RemoveDialogIsVisible = false;
			CancelOrderDialogIsVisible = false;
		}
		


		private void OnCheckout()
		{
			Question = _confirmQuestion;

			ConfirmDialogIsVisible = true;
			CancelOrderDialogIsVisible = false;
			RemoveDialogIsVisible = false;

			_eventAggregator.GetEvent<OpenDialogOverlayEvent>().Publish();
		}
		
		private void OnCancelOrder()
		{			
			Question = _cancelQuestion;

			ConfirmDialogIsVisible = false;
            CancelOrderDialogIsVisible = true;
			RemoveDialogIsVisible = false;

            _eventAggregator.GetEvent<OpenDialogOverlayEvent>().Publish();
		}

        private void OnRemoveItem(RemoveItemEventArgs args)
		{
			_orderItem = args.OrderItem;			

			Question = _removeQuestion;

			ConfirmDialogIsVisible = false;
			CancelOrderDialogIsVisible = false;
			RemoveDialogIsVisible = true;

            _eventAggregator.GetEvent<OpenDialogOverlayEvent>().Publish();
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
			CancelOrderButtonText = English.CancelOrder;
			RemoveButtonText = English.Remove;
			ConfirmButtonText = English.Confirm;
			AlternateCancelButtonText = English.AlternateCancel;
			CancelButtonText = English.Cancel;

			_confirmQuestion = English.ConfirmQuestion;
			_removeQuestion = English.RemoveQuestion;
			_cancelQuestion = English.CancelOrderQuestion;			
		}

		private void OnBosnianSelected()
		{
			CancelOrderButtonText = Bosnian.CancelOrder;
			RemoveButtonText = Bosnian.Remove;
			ConfirmButtonText = Bosnian.Confirm;
			CancelButtonText = Bosnian.Cancel;
			AlternateCancelButtonText = Bosnian.AlternateCancel;

			_confirmQuestion = Bosnian.ConfirmQuestion;
			_removeQuestion = Bosnian.RemoveQuestion;
			_cancelQuestion = Bosnian.CancelOrderQuestion;
		}

		private void OnGermanSelected()
		{
			CancelOrderButtonText = German.CancelOrder;
			RemoveButtonText = German.Remove;
			ConfirmButtonText = German.Confirm;
			CancelButtonText = German.Cancel;
			AlternateCancelButtonText = German.AlternateCancel;

			_confirmQuestion = German.ConfirmQuestion;
			_removeQuestion = German.RemoveQuestion;
			_cancelQuestion = German.CancelOrderQuestion;
		}



		private void Execute_Confirm()
		{
			_eventAggregator.GetEvent<CheckoutConfirmedEvent>().Publish();            
			
			_eventAggregator.GetEvent<PaymentOptionsViewEvent>().Publish();            
			
			_eventAggregator.GetEvent<CloseDialogOverlayEvent>().Publish();
		}

		private void Execute_CancelOrder()
		{
            _eventAggregator.GetEvent<CancelOrderConfirmedEvent>().Publish();

			_eventAggregator.GetEvent<FoodDrinkViewEvent>().Publish();            

            _eventAggregator.GetEvent<CloseDialogOverlayEvent>().Publish();
		}
		
		private void Execute_Remove()
		{
            _eventAggregator.GetEvent<RemoveItemConfirmedEvent>().Publish(new RemoveItemConfirmedEventArgs
            {
                Id = _orderItem.Id,
                OrderItem = new OrderItem
                {
                    Id = _orderItem.Id,
                    Description = _orderItem.Description,
                    Name = _orderItem.Description,
                    Image = _orderItem.Image,
                    Price = _orderItem.Price
				}
            });
			
			_eventAggregator.GetEvent<CloseDialogOverlayEvent>().Publish();
        }

		private void Execute_Close()
		{
			_eventAggregator.GetEvent<CloseDialogOverlayEvent>().Publish();
		}

		#endregion
	}
}