using System.Windows.Input;
using SEFLink.UI.Events;
using Prism.Commands;
using Prism.Events;
using System;
using SEFLink.UI.HCI.Events;

namespace SEFLink.UI.ViewModels
{
    public class FirstSettingsViewModel : Observable
    {
        #region Fields

        private string _incomingFilesLocation;
        private string _outgoingFilesLocation;
        private string _apiKey;

        private bool _automaticSending;
        private bool _automaticConversion;

        private IEventAggregator _eventAggregator;

        public ICommand NextCommand { get; }
        public ICommand ChooseInFolderCommand { get; }
        public ICommand ChooseOutFolderCommand { get; }

        #endregion


        #region Constructor

        public FirstSettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NextCommand = new DelegateCommand(Execute_Next, CanExecute_Next);
            ChooseInFolderCommand = new DelegateCommand(Execute_ChooseInFolder);
            ChooseOutFolderCommand = new DelegateCommand(Execute_ChooseOutFolder);

            LoadDefaultData();

            _eventAggregator.GetEvent<PreviousSettingsViewEvent>().Subscribe(OnPreviousSettingsView);
            _eventAggregator.GetEvent<LoggedInEvent>().Subscribe(OnLoggedIn);
        }

        #endregion


        #region Properties

        public string APIKey
        {
            get { return _apiKey; }
            set
            {
                _apiKey = value;
                OnPropertyChanged();
            }
        }

        public string IncomingFilesLocation
        {
            get { return _incomingFilesLocation; }
            set
            {
                _incomingFilesLocation = value;
                OnPropertyChanged();
                ((DelegateCommand)NextCommand).RaiseCanExecuteChanged();
            }
        }

        public string OutgoingFilesLocation
        {
            get { return _outgoingFilesLocation; }
            set
            {
                _outgoingFilesLocation = value;
                OnPropertyChanged();
                ((DelegateCommand)NextCommand).RaiseCanExecuteChanged();
            }
        }

        public bool AutomaticSending
        {
            get { return _automaticSending; }
            set { _automaticSending = value; OnPropertyChanged(); }
        }

        public bool AutomaticConversion
        {
            get { return _automaticConversion; }
            set { _automaticConversion = value; OnPropertyChanged(); }
        }

        #endregion


        #region Other Methods

        private void LoadDefaultData()
        {
            IncomingFilesLocation = "C:\\SEFLink\\Incoming";
            OutgoingFilesLocation = "C:\\SEFLink\\Outgoing";

            AutomaticSending = true;
            AutomaticConversion = true;

            //APIKey = "9c127b1b-6290-4754-9434-ee8344c48321";
        }

        private void Execute_Next()
        {
            _eventAggregator.GetEvent<NextSettingsViewEvent>().Publish(
                new NextSettingsViewEventArgs
                {
                    APIKey = APIKey,
                    IncomingFilesLocation = IncomingFilesLocation,
                    OutgoingFilesLocation = OutgoingFilesLocation,
                    AutoConversion = AutomaticConversion,
                    AutoSending = AutomaticSending
                });
        }

        private bool CanExecute_Next()
        {
            return IncomingFilesLocation.Length != 0 && OutgoingFilesLocation.Length != 0;
        }

        private void Execute_ChooseOutFolder()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                OutgoingFilesLocation = dialog.SelectedPath;
            }
        }

        private void Execute_ChooseInFolder()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                IncomingFilesLocation = dialog.SelectedPath;
            }
        }

        private void OnLoggedIn(LoggedInEventArgs args)
        {

            if (args.UserSettings != null)
            {
                APIKey = args.UserSettings.API_Key;
                IncomingFilesLocation = args.UserSettings.IncomingFilesLocation;
                OutgoingFilesLocation = args.UserSettings.OutgoingFilesLocation;
                AutomaticSending = args.UserSettings.AutomaticSending;

                AutomaticConversion = args.UserSettings.AutomaticConversion_IncomingFiles || args.UserSettings.AutomaticConversion_OutgoingFiles;
            }
            else
            {
                IncomingFilesLocation = $"C:\\SEFLink\\{args.UserName}\\Incoming";
                OutgoingFilesLocation = $"C:\\SEFLink\\{args.UserName}\\Outgoing";

                APIKey = "";

                AutomaticSending = true;
                AutomaticConversion = true;
            }

        }

        private void OnPreviousSettingsView(PreviousSettingsViewEventArgs args)
        {
            AutomaticConversion = args.AutoConversion_IncomingFiles || args.AutoConversion_OutgoingFiles;

            IncomingFilesLocation = args.IncomingFilesLocation;
            OutgoingFilesLocation = args.OutgoingFilesLocation;
        }

        #endregion
    }
}
