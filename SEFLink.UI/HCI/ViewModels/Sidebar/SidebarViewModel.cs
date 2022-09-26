using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Data;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static SEFLink.UI.HCI.Constants.LanguageConstants;

namespace SEFLink.UI.HCI.ViewModels.Menu
{
    public class SidebarViewModel : Observable
    {
        #region Fields

        private string _titleText;
        private string _checkoutText;
        private string _subTotalText;
        private string _price;

        private bool _checkoutIsVisible;
        private bool _cancelIsVisible;
        private bool _undoIsVisible;

        private readonly IEventAggregator _eventAggregator;
        private UndoStack _undoStack;


        public ICommand CheckoutCommand { get; }
        public ICommand CancelOrderCommand { get; }
        public ICommand UndoCommand { get; }

        #endregion


        #region Constructor

        public SidebarViewModel(IEventAggregator eventAggregator, UndoStack undoStack)
        {
            _eventAggregator = eventAggregator;
            _undoStack = undoStack;


            Setup();

            CheckoutCommand = new DelegateCommand(Execute_Checkout, CanExecute_Checkout);
            CancelOrderCommand = new DelegateCommand(Execute_CancelOrder, CanExecute_CancelOrder);
            UndoCommand = new DelegateCommand(Execute_Undo, CanExecute_Undo);

            _eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);
            _eventAggregator.GetEvent<AddItemEvent>().Subscribe(OnItemAdded);
            _eventAggregator.GetEvent<RemoveItemConfirmedEvent>().Subscribe(OnItemRemoved);
            _eventAggregator.GetEvent<CheckoutConfirmedEvent>().Subscribe(OnCheckout);
            _undoStack = undoStack;
        }

        #endregion


        #region Properties

        public ObservableCollection<OrderItemViewModel> OrderItems { get; set; }
        
        public string Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        public string TitleText
        {
            get { return _titleText; }
            set { _titleText = value; OnPropertyChanged(); }
        }

        public string SubTotalText
        {
            get { return _subTotalText; }
            set { _subTotalText = value; OnPropertyChanged(); }
        }

        public string CheckoutText
        {
            get { return _checkoutText; }
            set { _checkoutText = value; OnPropertyChanged(); }
        }

        public bool CheckoutIsVisible
        {
            get { return _checkoutIsVisible; }
            set { _checkoutIsVisible = value; OnPropertyChanged(); }
        }

        public bool CancelIsVisible
        {
            get { return _cancelIsVisible; }
            set { _cancelIsVisible = value; OnPropertyChanged(); }
        }
        
        public bool UndoIsVisible
        {
            get { return _undoIsVisible; }
            set { _undoIsVisible = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        private void Setup()
        {
            OrderItems = new ObservableCollection<OrderItemViewModel>();

            TitleText = "";
            Price = $"{0.00M}";
            CheckoutIsVisible = true;
            CancelIsVisible = true;
            UndoIsVisible = false;

            OnEnglishSelected();
        }

        private async void RefreshView()
        {
            await Task.Delay(100);

            UndoIsVisible = _undoStack.OrderItems.Count > 0;
        }
        
        private void OnCheckout(CheckoutConfirmedEventArgs args)
        {

        }

        private void OnItemRemoved(RemoveItemConfirmedEventArgs args)
        {
            var OrderItemsCopy = OrderItems.Where(oi => oi.Id != args.Id).ToList();

            OrderItems.Clear();

            foreach (var item in OrderItemsCopy)
            {
                OrderItems.Add(item);
            }

            OrderItemsCopy.Clear();

            CalculateTotalPrice();

            RefreshView();
        }

        private void OnItemAdded(AddItemEventArgs args)
        {
            OrderItems.Add(OrderItemHelper.CreateOrderItemViewModel(_eventAggregator, args.OrderItem));

            CalculateTotalPrice();

            RefreshView();
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

        private void CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var item in OrderItems)
            {
                totalPrice += item.Price;
            }

            if (totalPrice <= 0)
            {
                totalPrice = 0.00M;
            }

            Price = $"{totalPrice}";
        }

        private void OnEnglishSelected()
        {
            CheckoutText = English.Checkout;
            SubTotalText = English.SubTotal;
            TitleText = English.YourOrder;
        }

        private void OnBosnianSelected()
        {
            CheckoutText = Bosnian.Checkout;
            SubTotalText = Bosnian.SubTotal;
            TitleText = Bosnian.YourOrder;
        }

        private void OnGermanSelected()
        {
            CheckoutText = German.Checkout;
            SubTotalText = German.SubTotal;
            TitleText = German.YourOrder;
        }

        private void Execute_Checkout()
        {
            _eventAggregator.GetEvent<CheckoutEvent>().Publish(new CheckoutEventArgs());

            RefreshView();
        }

        private void Execute_CancelOrder()
        {
            _eventAggregator.GetEvent<CancelOrderEvent>().Publish(new CancelOrderEventArgs());

            RefreshView();
        }

        private void Execute_Undo()
        {
            _eventAggregator.GetEvent<UndoRemoveItemEvent>().Publish(new UndoRemoveItemEventArgs());

            RefreshView();
        }

        private bool CanExecute_Checkout() => true;

        private bool CanExecute_CancelOrder() => true;
        
        private bool CanExecute_Undo() => true;

        #endregion
    }
}