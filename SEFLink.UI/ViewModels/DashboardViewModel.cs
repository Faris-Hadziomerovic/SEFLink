using Prism.Commands;
using Prism.Events;
using SEFLink.Model;
using SEFLink.UI.Data;
using SEFLink.UI.Events;
using SEFLink.UI.ViewModels.Dashboard;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SEFLink.UI.ViewModels
{
    public class DashboardViewModel : Observable
    {
        #region Fields

        private UserDocuments _userDocuments;

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
                                  IncomingDocumentsViewModel incomingDocumentsViewModel,
                                  OutgoingDocumentsViewModel outgoingDocumentsViewModel,
                                  IUserDocumentsDataService userDocumentsDataService,
                                  IEventAggregator eventAggregator)
        {
            BaseTitleBarViewModel = baseTitleBarViewModel;
            _eventAggregator = eventAggregator;
            _userDocumentsDataService = userDocumentsDataService;

            ShowOutgoingDocsCommand = new DelegateCommand(Execute_ShowOutgoingDocs, CanExecute_ShowOutgoingDocs);
            ShowIncomingDocsCommand = new DelegateCommand(Execute_ShowIncomingDocs, CanExecute_ShowIncomingDocs);

            LogOutCommand = new DelegateCommand(Execute_LogOut, CanExecute_LogOut);

            IncomingDocumentsViewModel = incomingDocumentsViewModel;
            OutgoingDocumentsViewModel = outgoingDocumentsViewModel;

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
            set { _selectedDocumentType = value; OnPropertyChanged(); }
        }        

        public string DocumentNumber
        {
            get { return _documentNumber; }
            set { _documentNumber = value; OnPropertyChanged(); }
        }

        public string DocumentDateString
        {
            get { return _docDateString; }
            set { _docDateString = value; OnPropertyChanged(); }
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
            set { _searchTerm = value; OnPropertyChanged(); }
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

            DocumentDateString = "";

            CurrentViewModel = null;

            DocumentTypes = new ObservableCollection<string> { "Izaberi tip dokumenta", "TIP", "KO", "KF", "KZ", "AF" };

            SelectedDocumentType = DocumentTypes[0];
        }

        private void OnLoggedIn(LoggedInEventArgs args)
        {
            UserDocuments = _userDocumentsDataService.GetUserDocuments(args.Id);

            IncomingDocumentsViewModel = new IncomingDocumentsViewModel(UserDocuments.GetDocuments(), _eventAggregator);
            OutgoingDocumentsViewModel = new OutgoingDocumentsViewModel(UserDocuments.GetDocuments(), _eventAggregator);

            LoadDefaultData();
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
        }

        private bool CanExecute_LogOut()
        {
            return true;
        }

        #endregion
    }
}
