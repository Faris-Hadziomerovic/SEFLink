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

		private string _cancelButtonText;
		private string _removeButtonText;
		private string _confirmButtontext;

		private bool _confirmDialogIsVisible;
		private bool _removeDialogIsVisible;
		private bool _cancelOrderDialogIsVisible;

		private readonly IEventAggregator _eventAggregator;

		public ICommand ConfirmCommand { get; }
		public ICommand CancelCommand { get; }
		public ICommand CancelOrderCommand { get; }
		public ICommand RemoveCommand { get; }

		private OrderItem _orderItem;

		#endregion


		#region Constructor

		public DialogOverlayViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;

			Setup();

			ConfirmCommand = new DelegateCommand(Execute_Confirm);
			CancelCommand = new DelegateCommand(Execute_Cancel);
			CancelOrderCommand = new DelegateCommand(Execute_CancelOrder);
			RemoveCommand = new DelegateCommand(Execute_Remove);

			_eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);

			_eventAggregator.GetEvent<RemoveItemEvent>().Subscribe(OnRemoveItem);
			_eventAggregator.GetEvent<CheckoutEvent>().Subscribe(OnCheckout);
			_eventAggregator.GetEvent<CancelOrderEvent>().Subscribe(OnCancelOrder);
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

		public string CancelButtonText
		{
			get { return _cancelButtonText; }
			set { _cancelButtonText = value; OnPropertyChanged(); }
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
		
		private void OnCheckout(CheckoutEventArgs args)
		{
			Question = _confirmQuestion;

			ConfirmDialogIsVisible = true;
			RemoveDialogIsVisible = false;
			CancelOrderDialogIsVisible = false;

			_eventAggregator.GetEvent<OpenDialogOverlayEvent>().Publish();
		}

        private void OnRemoveItem(RemoveItemEventArgs args)
		{
			_orderItem = args.OrderItem;			

			Question = _removeQuestion;

			ConfirmDialogIsVisible = false;
			RemoveDialogIsVisible = true;
			CancelOrderDialogIsVisible = false;

            _eventAggregator.GetEvent<OpenDialogOverlayEvent>().Publish();
		}
		
		private void OnCancelOrder(CancelOrderEventArgs args)
		{			
			Question = _cancelQuestion;

			ConfirmDialogIsVisible = false;
			RemoveDialogIsVisible = false;
            CancelOrderDialogIsVisible = true;

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
			CancelButtonText = English.Cancel;
			RemoveButtonText = English.Remove;
			ConfirmButtonText = English.Confirm;

			_confirmQuestion = English.ConfirmQuestion;
			_removeQuestion = English.RemoveQuestion;
			_cancelQuestion = English.CancelOrderQuestion;			
		}

		private void OnBosnianSelected()
		{
			CancelButtonText = Bosnian.Cancel;
			RemoveButtonText = Bosnian.Remove;
			ConfirmButtonText = Bosnian.Confirm;

			_confirmQuestion = Bosnian.ConfirmQuestion;
			_removeQuestion = Bosnian.RemoveQuestion;
			_cancelQuestion = Bosnian.CancelOrderQuestion;
		}

		private void OnGermanSelected()
		{
			CancelButtonText = German.Cancel;
			RemoveButtonText = German.Remove;
			ConfirmButtonText = German.Confirm;

			_confirmQuestion = German.ConfirmQuestion;
			_removeQuestion = German.RemoveQuestion;
			_cancelQuestion = German.CancelOrderQuestion;
		}



		private void Execute_Confirm()
		{
			_eventAggregator.GetEvent<CheckoutConfirmedEvent>().Publish(new CheckoutConfirmedEventArgs());            
			
			_eventAggregator.GetEvent<CloseDialogOverlayEvent>().Publish();
		}

		private void Execute_Cancel()
		{
			_eventAggregator.GetEvent<CloseDialogOverlayEvent>().Publish();
		}
		
		private void Execute_CancelOrder()
		{
            _eventAggregator.GetEvent<CancelOrderEvent>().Publish(new CancelOrderEventArgs());

            _eventAggregator.GetEvent<CloseDialogOverlayEvent>().Publish();
		}

		private void Execute_Remove()
		{
            _eventAggregator.GetEvent<RemoveItemConfirmedEvent>().Publish(new RemoveItemConfirmedEventArgs
            {
                Id = _orderItem.Id,
                OrderItem = new Models.OrderItem
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

		private bool CanExecute_Checkout() => true;

		private bool CanExecute_CancelOrder() => true;

		private bool CanExecute_Undo() => true;

		#endregion
	}
}