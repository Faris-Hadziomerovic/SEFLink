using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using SEFLink.Model;
using SEFLink.UI.Data;
using SEFLink.UI.Events;
using System.Collections.Generic;

namespace SEFLink.UI.ViewModels.Dashboard
{
    public class OutgoingDocumentsViewModel : Observable
    {
        #region Fields

        private FilterEventArgs _filterArgs;

        private bool _filterIsActive;
        private int _showFrom;
        private int _showTo;
        private int _totalNumberOfDocuments;
        private int _totalNumberOfFilteredDocuments;
        private int _numberOfDocumentsPerPage;
        private string _selectedAction;
        private int _notificationNumber;
        private bool _selectedAll;

        public bool PageNumberingVisibility { get => ! FilterIsActive; }
        public bool NotificationVisibility { get => NotificationNumber > 0; }

        private IDocumentInfoDataService _documentInfoDataService;
        private IEventAggregator _eventAggregator;

        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand ApplyActionCommand { get; }

        public ICommand NotificationsCommand { get; }

        public ICommand RefreshCommand { get; }
        public ICommand SortCommand { get; }

        #endregion

        

        #region Constructor

        public OutgoingDocumentsViewModel(IDocumentInfoDataService documentInfoDataService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            DocumentInfoDataService = documentInfoDataService;

            NextPageCommand = new DelegateCommand(Execute_NextPage, CanExecute_NextPage);
            PreviousPageCommand = new DelegateCommand(Execute_PreviousPage, CanExecute_PreviousPage);

            ApplyActionCommand = new DelegateCommand(Execute_ApplyAction, CanExecute_ApplyAction);
            NotificationsCommand = new DelegateCommand(Execute_Notifications, CanExecute_Notifications);

            SortCommand = new DelegateCommand(Execute_Sort, CanExecute_Sort);
            RefreshCommand = new DelegateCommand(Execute_Refresh, CanExecute_Refresh);


            LoadViewData();


            _eventAggregator.GetEvent<NotificationsClickedEvent>().Subscribe(OnNotificationsClicked);
            _eventAggregator.GetEvent<FilterEvent>().Subscribe(OnFilterApplied);
        }
        
        #endregion



        #region Properties

        public ObservableCollection<DocumentInfoViewModel> ShownDocuments { get; set; }

        public List<DocumentInfo> FilteredDocuments { get; set; }

        public ObservableCollection<string> Actions { get; set; }

        public IDocumentInfoDataService DocumentInfoDataService
        {
            get { return _documentInfoDataService; }
            set { _documentInfoDataService = value; OnPropertyChanged(); }
        }

        public string SelectedAction
        {
            get { return _selectedAction; }
            set
            {
                _selectedAction = value;

                ((DelegateCommand)ApplyActionCommand).RaiseCanExecuteChanged();

                OnPropertyChanged();
            }
        }

        public bool FilterIsActive
        {
            get { return _filterIsActive; }
            set
            {
                _filterIsActive = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(PageNumberingVisibility));

                ((DelegateCommand)PreviousPageCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NextPageCommand).RaiseCanExecuteChanged();
            }
        }

        public int ShowFrom
        {
            get { return _showFrom; }
            set
            {
                _showFrom = value;

                if (value < 1)
                    _showFrom = 1;

                if (_showFrom > TotalNumberOfDocuments)
                    _showFrom = TotalNumberOfDocuments;

                ((DelegateCommand)PreviousPageCommand).RaiseCanExecuteChanged();

                OnPropertyChanged();
            }
        }

        public int ShowTo
        {
            get { return _showTo; }
            set
            {
                _showTo = value;

                if (value < 1)
                    _showTo = 1;

                if (_showTo > TotalNumberOfDocuments)
                    _showTo = TotalNumberOfDocuments;

                ((DelegateCommand)NextPageCommand).RaiseCanExecuteChanged();

                OnPropertyChanged();
            }
        }

        public int NumberOfDocumentsPerPage
        {
            get { return _numberOfDocumentsPerPage; }
            set
            {
                _numberOfDocumentsPerPage = value;

                if (value < 1)
                    _numberOfDocumentsPerPage = 1;

                if (_numberOfDocumentsPerPage > TotalNumberOfDocuments)
                    _numberOfDocumentsPerPage = TotalNumberOfDocuments;

                OnPropertyChanged();
            }
        }

        public int TotalNumberOfDocuments
        {
            get { return _totalNumberOfDocuments; }
            private set { _totalNumberOfDocuments = value; OnPropertyChanged(); }
        }

        public int TotalNumberOfFilteredDocuments
        {
            get { return _totalNumberOfFilteredDocuments; }
            private set { _totalNumberOfFilteredDocuments = value; OnPropertyChanged(); }
        }

        public int NotificationNumber
        {
            get { return _notificationNumber; }
            set
            {
                _notificationNumber = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NotificationVisibility));

                ((DelegateCommand)NotificationsCommand).RaiseCanExecuteChanged();
            }
        }

        public bool SelectedAll
        {
            get { return _selectedAll; }
            set
            {
                _selectedAll = value;
                OnPropertyChanged();
                SelectAll();
            }
        }

        #endregion



        #region Methods

        private void LoadViewData()
        {
            FilteredDocuments = new List<DocumentInfo>();

            ShownDocuments = new ObservableCollection<DocumentInfoViewModel>();

            Actions = new ObservableCollection<string> { "Odaberi akciju", "Obriši", "Arhiviraj" };

            SelectedAction = Actions[0]; // "Odaberi akciju"

            NotificationNumber = DocumentInfoDataService.GetNotifications();

            TotalNumberOfDocuments = DocumentInfoDataService.GetOutgoingCount();

            NumberOfDocumentsPerPage = 5;

            ShowFrom = 1;
            ShowTo = ShowFrom + NumberOfDocumentsPerPage - 1;

            FilterIsActive = false;

            ShowDocuments();
        }

        public async void ShowDocuments()
        {
            ShownDocuments.Clear();

            SelectedAll = false;

            if (TotalNumberOfDocuments == 0 || NumberOfDocumentsPerPage == 0) return;

            var docs = await DocumentInfoDataService.GetAllOutgoingDocumentsAsync();

            for (int i = ShowFrom - 1; i <= ShowTo - 1; i++)
            {
                var item = DocumentInfoHelper.CreateDocumentInfoViewModel(docs[i], DocumentInfoType.Outgoing);

                ShownDocuments.Add(item);
            }
        }        


        private void OnFilterApplied(FilterEventArgs args)
        {
            _filterArgs = args;

            if (args.ResetFilter)
            {
                FilterIsActive = false;
                ShowDocuments();
            }
            else
            {
                FilterIsActive = true;
                FilterDocuments();
            }
        }

        private async void FilterDocuments()
        {
            FilteredDocuments.Clear();
            ShownDocuments.Clear();

            FilteredDocuments = await _documentInfoDataService.GetAllOutgoingDocumentsAsync();

            if (_filterArgs.Type != null)
            {
                FilteredDocuments = FilteredDocuments.Where(x => x.Type == _filterArgs.Type).ToList();
            }

            if (_filterArgs.Date != null)
            {
                FilteredDocuments = FilteredDocuments
                    .Where(x => x.Date.Year == _filterArgs.Date.Value.Year &&
                                x.Date.Month == _filterArgs.Date.Value.Month &&
                                x.Date.Day == _filterArgs.Date.Value.Day).ToList();
            }

            if (_filterArgs.Number != null)
            {
                FilteredDocuments = FilteredDocuments
                    .Where(x => x.NumberDisplayMember.ToLower().Contains( _filterArgs.Number.ToLower()) ).ToList();
            }

            if (_filterArgs.SearchTerm != null)
            {
                FilteredDocuments = FilteredDocuments
                    .Where(x => x.Receiver.ToLower().Contains( _filterArgs.SearchTerm.ToLower()) ).ToList();
            }

            TotalNumberOfFilteredDocuments = FilteredDocuments.Count();

            foreach (var document in FilteredDocuments)
            {
                var item = DocumentInfoHelper.CreateDocumentInfoViewModel(document, DocumentInfoType.Outgoing);

                ShownDocuments.Add(item);
            }
        }


        private bool CanExecute_PreviousPage()
        {
            return ShowFrom > 1 && !FilterIsActive;
        }

        private void Execute_PreviousPage()
        {
            ShowFrom -= NumberOfDocumentsPerPage;
            ShowTo = ShowFrom + NumberOfDocumentsPerPage - 1;

            ShowDocuments();
        }

        private bool CanExecute_NextPage()
        {
            return ShowTo < TotalNumberOfDocuments && !FilterIsActive;
        }

        private void Execute_NextPage()
        {
            ShowFrom += NumberOfDocumentsPerPage;
            ShowTo = ShowFrom + NumberOfDocumentsPerPage - 1;

            ShowDocuments();
        }

        private bool CanExecute_ApplyAction()
        {
            return SelectedAction == Actions[1] ||     // "Obriši"
                   SelectedAction == Actions[2];      // "Arhiviraj"
        }

        private void Execute_ApplyAction()
        {
            if (SelectedAction == Actions[1])         // "Obriši"
            {
                foreach (var Doc in ShownDocuments)
                {
                    if (Doc.IsSelected)
                    {
                        int Id = Doc.Id;

                        if (DocumentInfoDataService.DeleteOutgoingById(Id))
                        {
                            TotalNumberOfDocuments -= 1;
                        }
                    }
                }
            }


            if (SelectedAction == Actions[2])      // "Arhiviraj"
            {
                foreach (var Doc in ShownDocuments)
                {
                    if (Doc.IsSelected)
                    {
                        int Id = Doc.Id;

                        DocumentInfoDataService.GetOutgoingDocumentById(Id).Status = DocStatus.Archived;
                    }
                }
            }

            ShowTo = ShowFrom + NumberOfDocumentsPerPage - 1;

            Refresh();
        }

        private bool CanExecute_Notifications()
        {
            return NotificationNumber > 0;
        }

        private void Execute_Notifications()
        {
            NotificationNumber -= 1;

            _eventAggregator.GetEvent<NotificationsClickedEvent>().Publish(NotificationNumber);
        }

        private void OnNotificationsClicked(int args)
        {
            NotificationNumber = args;
        }

        private bool CanExecute_Refresh() => true;

        private void Execute_Refresh()
        {
            Refresh();
        }
        
        private bool CanExecute_Sort() => true;

        private void Execute_Sort()
        {
            var Docs = new ObservableCollection<DocumentInfoViewModel>(ShownDocuments.OrderBy(x => x.Endpoint));

            ShownDocuments.Clear();

            foreach (var doc in Docs)
                ShownDocuments.Add(doc);
        }

        private void SelectAll()
        {
            foreach (var item in ShownDocuments)
            {
                item.IsSelected = SelectedAll;
            }
        }

        private void Refresh()
        {
            SelectedAll = false;

            if (FilterIsActive)
            {
                FilterDocuments();
            }
            else
            {
                ShowDocuments();
            }
        }

        #endregion
    }
}
