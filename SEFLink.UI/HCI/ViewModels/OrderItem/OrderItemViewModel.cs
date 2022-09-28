using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using SEFLink.UI.Events;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.Models;

namespace SEFLink.UI.HCI.ViewModels
{
    public class OrderItemViewModel : Observable
    {
        #region Fields

        private Guid _id;

        private bool _isCartItem;

        private string _image;
        private string _name;
        private string _description;
        private decimal _price;
        private string _priceText;
        private int _numberOfItems;

        #region Image Flags 

        private bool _colaIsVisible;
        private bool _spriteIsVisible;
        private bool _fantaIsVisible;

        private bool _cafeLatteIsVisible;
        private bool _cappuccinoIsVisible;
        private bool _hotChocolateIsVisible;

        private bool _pizza1IsVisible;
        private bool _pizza2IsVisible;
        private bool _pizza3IsVisible;

        private bool _hamburgerIsVisible;
        private bool _chickenIsVisible;
        private bool _friesIsVisible;

        #endregion

        private IEventAggregator _eventAggregator;

        public ICommand RemoveItemCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand IncrementCommand { get; }
        public ICommand DecrementCommand { get; }

        #endregion



        #region Constructor

        public OrderItemViewModel(IEventAggregator eventAggregator, Guid id, string image, string name, string description, decimal price, bool isCartItem = true)
        {
            _eventAggregator = eventAggregator;

            Id = id;
            Image = image;
            Name = name;
            Description = description;
            Price = price;
            PriceText = $"{price}";


            RemoveItemCommand = new DelegateCommand(Execute_RemoveItem, CanExecute_RemoveItem);
            AddItemCommand = new DelegateCommand(Execute_AddItem, CanExecute_AddItem);
            IncrementCommand = new DelegateCommand(Execute_Increment, CanExecute_Increment);
            DecrementCommand = new DelegateCommand(Execute_Decrement, CanExecute_Decrement);

            _eventAggregator.GetEvent<ChangeLanguageEvent>().Subscribe(OnLanguageChanged);

            Setup();

            IsCartItem = isCartItem;
        }

        #endregion



        #region Properties

        public bool IsCartItem
        {
            get { return _isCartItem; }
            private set
            {
                _isCartItem = value;
                OnPropertyChanged();
                ((DelegateCommand)AddItemCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveItemCommand).RaiseCanExecuteChanged();

            }
        }

        public Guid Id
        {
            get => _id;
            private set => _id = value;
        }

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        public string PriceText
        {
            get { return _priceText; }
            set { _priceText = value; OnPropertyChanged(); }
        }

        public int NumberOfItems
        {
            get { return _numberOfItems; }
            set
            {
                _numberOfItems = value;
                OnPropertyChanged();
                ((DelegateCommand)DecrementCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)IncrementCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddItemCommand).RaiseCanExecuteChanged();
            }
        }


        #region Image Flags 

        public bool ColaIsVisible
        {
            get { return _colaIsVisible; }
            set { _colaIsVisible = value; OnPropertyChanged(); }
        }

        public bool SpriteIsVisible
        {
            get { return _spriteIsVisible; }
            set { _spriteIsVisible = value; OnPropertyChanged(); }
        }

        public bool FantaIsVisible
        {
            get { return _fantaIsVisible; }
            set { _fantaIsVisible = value; OnPropertyChanged(); }
        }

        public bool CafeLatteIsVisible
        {
            get { return _cafeLatteIsVisible; }
            set { _cafeLatteIsVisible = value; OnPropertyChanged(); }
        }

        public bool CappuccinoIsVisible
        {
            get { return _cappuccinoIsVisible; }
            set { _cappuccinoIsVisible = value; OnPropertyChanged(); }
        }

        public bool HotChocolateIsVisible
        {
            get { return _hotChocolateIsVisible; }
            set { _hotChocolateIsVisible = value; OnPropertyChanged(); }
        }


        public bool Pizza1IsVisible
        {
            get { return _pizza1IsVisible; }
            set { _pizza1IsVisible = value; OnPropertyChanged(); }
        }

        public bool Pizza2IsVisible
        {
            get { return _pizza2IsVisible; }
            set { _pizza2IsVisible = value; OnPropertyChanged(); }
        }

        public bool Pizza3IsVisible
        {
            get { return _pizza3IsVisible; }
            set { _pizza3IsVisible = value; OnPropertyChanged(); }
        }

        public bool HamburgerIsVisible
        {
            get { return _hamburgerIsVisible; }
            set { _hamburgerIsVisible = value; OnPropertyChanged(); }
        }

        public bool ChickenIsVisible
        {
            get { return _chickenIsVisible; }
            set { _chickenIsVisible = value; OnPropertyChanged(); }
        }

        public bool FriesIsVisible
        {
            get { return _friesIsVisible; }
            set { _friesIsVisible = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion



        #region Methods

        private void Setup()
        {
            NumberOfItems = 1;

            ColaIsVisible = false;
            SpriteIsVisible = false;
            FantaIsVisible = false;

            CafeLatteIsVisible = false;
            CappuccinoIsVisible = false;
            HotChocolateIsVisible = false;

            Pizza1IsVisible = false;
            Pizza2IsVisible = false;
            Pizza3IsVisible = false;

            HamburgerIsVisible = false;
            ChickenIsVisible = false;
            FriesIsVisible = false;

            switch (Image)
            {
                case "Hamburger":
                    HamburgerIsVisible = true;
                    break;

                case "Chicken":
                    ChickenIsVisible = true;
                    break;

                case "Fries":
                    FriesIsVisible = true;
                    break;

                case "Pizza1":
                    Pizza1IsVisible = true;
                    break;

                case "Pizza2":
                    Pizza2IsVisible = true;
                    break;

                case "Pizza3":
                    Pizza3IsVisible = true;
                    break;

                case "Cola":
                    ColaIsVisible = true;
                    break;

                case "Sprite":
                    SpriteIsVisible = true;
                    break;

                case "Fanta":
                    FantaIsVisible = true;
                    break;

                case "CafeLatte":
                    CafeLatteIsVisible = true;
                    break;

                case "Cappuccino":
                    CappuccinoIsVisible = true;
                    break;

                case "HotChocolate":
                    HotChocolateIsVisible = true;
                    break;

                default:
                    HamburgerIsVisible = true;
                    break;
            }
        }

        private void OnLanguageChanged(ChangeLanguageEventArgs args)
        {

        }

        private void Execute_RemoveItem()
        {
            _eventAggregator.GetEvent<RemoveItemEvent>().Publish(new RemoveItemEventArgs
            {
                Id = _id,
                OrderItem = new OrderItem
                {
                    Id = _id,
                    Description = Description,
                    Name = Description,
                    Image = Image,
                    Price = Price
                }
            });

            //_eventAggregator.GetEvent<RemoveItemConfirmedEvent>().Publish(new RemoveItemConfirmedEventArgs
            //{
            //    Id = _id,
            //    OrderItem = new Models.OrderItem
            //    {
            //        Id = Id,
            //        Description = Description,
            //        Name = Description,
            //        Image = Image,
            //        Price = Price
            //    }
            //});
        }

        private void Execute_AddItem()
        {
            for (int i = 0; i < NumberOfItems; i++)
            {
                _eventAggregator.GetEvent<AddItemEvent>().Publish(new AddItemEventArgs
                {
                    UndoExecuted = false,
                    OrderItem = new OrderItem
                    {
                        Image = Image,
                        Name = Name,
                        Description = Description,
                        Price = Price
                    }
                });
            }

            NumberOfItems = 0;
        }

        private void Execute_Increment() => NumberOfItems++;

        private void Execute_Decrement() => NumberOfItems--;

        private bool CanExecute_RemoveItem() => IsCartItem;
        private bool CanExecute_AddItem() => !IsCartItem && NumberOfItems > 0;
        private bool CanExecute_Increment() => NumberOfItems <= 5;
        private bool CanExecute_Decrement() => NumberOfItems > 0;

        #endregion
    }
}
