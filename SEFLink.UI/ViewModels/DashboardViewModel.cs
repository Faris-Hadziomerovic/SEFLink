using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using SEFLink.Model;
using SEFLink.UI.Data;
using SEFLink.UI.Events;
using SEFLink.UI.ViewModels.Dashboard;

namespace SEFLink.UI.ViewModels
{
    public class DashboardViewModel : Observable
    {
        #region Fields

        private UserDocuments _userDocuments;

        private bool _canFilter = false; // stops unnecessary event publishing

        private string _selectedDocumentType;
        private string _documentNumber;
        private DateTime _documentDate;
        private string _docDateString;
        private string _searchTerm;

        private bool _incomingDocsButtonPressed;
        private bool _outgoingDocsButtonPressed;

        public bool IncomingDocsButtonVisible { get => !_incomingDocsButtonPressed; }
        public bool OutgoingDocsButtonVisible { get => !_outgoingDocsButtonPressed; }

        private BaseTitleBarViewModel _baseTitleBarViewModel;
        private IEventAggregator _eventAggregator;
        private IUserDocumentsDataService _userDocumentsDataService;
        private object _currentViewModel;
        private IncomingDocumentsViewModel _incomingDocumentsViewModel;
        private OutgoingDocumentsViewModel _outgoingDocumentsViewModel;

        public ICommand ShowOutgoingDocsCommand { get; }
        public ICommand ShowIncomingDocsCommand { get; }

        public ICommand LogOutCommand { get; }

        #endregion


        #region Constructor

        public DashboardViewModel(BaseTitleBarViewModel baseTitleBarViewModel,                                  
                                  IUserDocumentsDataService userDocumentsDataService,
                                  IEventAggregator eventAggregator)
        {
            BaseTitleBarViewModel = baseTitleBarViewModel;
            _eventAggregator = eventAggregator;
            _userDocumentsDataService = userDocumentsDataService;

            ShowOutgoingDocsCommand = new DelegateCommand(Execute_ShowOutgoingDocs, CanExecute_ShowOutgoingDocs);
            ShowIncomingDocsCommand = new DelegateCommand(Execute_ShowIncomingDocs, CanExecute_ShowIncomingDocs);

            LogOutCommand = new DelegateCommand(Execute_LogOut, CanExecute_LogOut);
            
            LoadDefaultData();

            _eventAggregator.GetEvent<LoggedInEvent>().Subscribe(OnLoggedIn);

        }

        #endregion


        #region Properties

        public ObservableCollection<string> DocumentTypes { get; set; }

        public UserDocuments UserDocuments
        {
            get { return _userDocuments; }
            set { _userDocuments = value; OnPropertyChanged(); }
        }

        public string SelectedDocumentType
        {
            get { return _selectedDocumentType; }
            set
            {
                _selectedDocumentType = value;
                OnPropertyChanged();

                if (ResetFilter() == false)
                {
                    Filter();
                }
            }
        }

        public string DocumentNumber
        {
            get { return _documentNumber; }
            set
            {
                _documentNumber = value;
                OnPropertyChanged();

                if (ResetFilter() == false)
                {
                    Filter();
                }
            }
        }

        public string DocumentDateString
        {
            get { return _docDateString; }
            set
            {
                _docDateString = value;
                OnPropertyChanged();

                if (string.IsNullOrWhiteSpace(value))
                {
                    if (ResetFilter() == false)
                    {
                        Filter();
                    }
                }
                else
                {
                    DocumentDate = DateTime.Parse(value);

                    Filter();
                }
            }
        }

        public DateTime DocumentDate
        {
            get { return _documentDate; }
            set
            {
                _documentDate = value;
                OnPropertyChanged();
            }
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged();

                if (ResetFilter() == false)
                {
                    Filter();
                }
            }
        }

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public BaseTitleBarViewModel BaseTitleBarViewModel
        {
            get { return _baseTitleBarViewModel; }
            set { _baseTitleBarViewModel = value; OnPropertyChanged(); }
        }

        public OutgoingDocumentsViewModel OutgoingDocumentsViewModel
        {
            get { return _outgoingDocumentsViewModel; }
            set { _outgoingDocumentsViewModel = value; OnPropertyChanged(); }
        }

        public IncomingDocumentsViewModel IncomingDocumentsViewModel
        {
            get { return _incomingDocumentsViewModel; }
            set { _incomingDocumentsViewModel = value; OnPropertyChanged(); }
        }

        public bool IncomingDocsButtonPressed
        {
            get { return _incomingDocsButtonPressed; }
            set
            {
                _incomingDocsButtonPressed = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IncomingDocsButtonVisible));

                ((DelegateCommand)ShowIncomingDocsCommand).RaiseCanExecuteChanged();
            }
        }

        public bool OutgoingDocsButtonPressed
        {
            get { return _outgoingDocsButtonPressed; }
            set
            {
                _outgoingDocsButtonPressed = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(OutgoingDocsButtonVisible));

                ((DelegateCommand)ShowOutgoingDocsCommand).RaiseCanExecuteChanged();

            }
        }

        #endregion


        #region Methods        

        private void LoadDefaultData()
        {
            IncomingDocsButtonPressed = false;
            OutgoingDocsButtonPressed = false;

            DocumentTypes = new ObservableCollection<string> { "Izaberi tip dokumenta", "TIP", "KO", "KF", "KZ", "AF" };

            SelectedDocumentType = DocumentTypes[0];

            DocumentNumber = "";

            DocumentDateString = "";

            SearchTerm = "";

            CurrentViewModel = null;

        }

        private async void OnLoggedIn(LoggedInEventArgs args)
        {
            UserDocuments = await _userDocumentsDataService.GetUserDocumentsAsync(args.Id);
            IDocumentInfoDataService Documents = await _userDocumentsDataService.GetDocumentsByUserIdAsync(args.Id);

            IncomingDocumentsViewModel = new IncomingDocumentsViewModel(Documents, _eventAggregator);
            OutgoingDocumentsViewModel = new OutgoingDocumentsViewModel(Documents, _eventAggregator);

            LoadDefaultData();

            _canFilter = true;
        }


        private bool Filter()
        {
            if (_canFilter == false)
            {
                return false;
            }

            var filter_args = GetFilterArgs();

            //Filter_Data(filter_args);

            _eventAggregator.GetEvent<FilterEvent>().Publish(filter_args);

            return true;
        }

        private bool ResetFilter()
        {
            if (_canFilter == false)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(DocumentNumber) &&
                string.IsNullOrWhiteSpace(DocumentDateString) &&
                string.IsNullOrWhiteSpace(SearchTerm) &&
                SelectedDocumentType == DocumentTypes[0])
            {
                //System.Windows.Forms.MessageBox.Show("reset filter");

                _eventAggregator.GetEvent<FilterEvent>()
                                .Publish(new FilterEventArgs { ResetFilter = true });

                return true;
            }

            return false;
        }

        private FilterEventArgs GetFilterArgs()
        {
            string number = null;
            string searchterm = null;
            DateTime? date = null;
            DocType? type = null;

            if (!string.IsNullOrWhiteSpace(DocumentNumber))
            {
                number = DocumentNumber;
            }

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                searchterm = SearchTerm;
            }

            if (!string.IsNullOrWhiteSpace(DocumentDateString))
            {
                date = DocumentDate;
            }

            if (SelectedDocumentType != DocumentTypes[0])
            {
                type = (DocType)Enum.Parse(typeof(DocType), SelectedDocumentType, true);
            }

            return new FilterEventArgs
            {
                ResetFilter = false,
                Number = number,
                Date = date,
                SearchTerm = searchterm,
                Type = type
            };
        }

        private static void Filter_Data(FilterEventArgs filter_args)
        {
            string message = "Number: ";

            if (filter_args.Number != null)
            {
                message += filter_args.Number;
            }
            else
            {
                message += "null";
            }

            message += "\nType: ";

            if (filter_args.Type != null)
            {
                message += filter_args.Type;
            }
            else
            {
                message += "null";
            }

            message += "\nDate: ";

            if (filter_args.Date != null)
            {
                message += filter_args.Date;
            }
            else
            {
                message += "null";
            }

            message += "\nSearch: ";

            if (filter_args.SearchTerm != null)
            {
                message += filter_args.SearchTerm;
            }
            else
            {
                message += "null";
            }



            System.Windows.Forms.MessageBox.Show(message, "Filter data");
        }


        private void Execute_ShowOutgoingDocs()
        {
            CurrentViewModel = OutgoingDocumentsViewModel;

            OutgoingDocsButtonPressed = true;
            IncomingDocsButtonPressed = false;
        }

        private bool CanExecute_ShowOutgoingDocs()
        {
            return !OutgoingDocsButtonPressed;
        }


        private void Execute_ShowIncomingDocs()
        {
            CurrentViewModel = IncomingDocumentsViewModel;

            IncomingDocsButtonPressed = true;
            OutgoingDocsButtonPressed = false;

        }

        private bool CanExecute_ShowIncomingDocs()
        {
            return !IncomingDocsButtonPressed;
        }


        private void Execute_LogOut()
        {
            _eventAggregator.GetEvent<LoggedOutEvent>().Publish();

            _canFilter = false;
        }

        private bool CanExecute_LogOut()
        {
            return true;
        }

        #endregion
    }
}
