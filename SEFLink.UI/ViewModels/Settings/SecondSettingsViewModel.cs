using System;
using System.Windows.Input;
using SEFLink.UI.Events;
using Prism.Commands;
using Prism.Events;

namespace SEFLink.UI.ViewModels
{
    public class SecondSettingsViewModel : Observable
    {
        #region Fields

        private string _incomingFilesLocation;
        private string _outgoingFilesLocation;

        private string _customIncomingFormat;
        private string _customOutgoingFormat;

        private string _apiKey;        
        private bool _automaticSending;

        private bool _automaticConversion_OutgoingFiles;
        private bool _automaticConversion_IncomingFiles;

        private string _customInFormatTextboxVisibility;
        private string _customOutFormatTextboxVisibility;

        private IEventAggregator _eventAggregator;

        public ICommand PreviousCommand { get; }
        public ICommand FinishCommand { get; }

        public ICommand ChooseInFolderCommand { get; }
        public ICommand ChooseOutFolderCommand { get; }

        public ICommand OtherFormatInCommand { get; }
        public ICommand OtherFormatOutCommand { get; }

        #region radio buttons

        private bool _incomingToXML;
        private bool _incomingToTXT;
        private bool _incomingToCSU;

        private bool _outgoingToXML;
        private bool _outgoingToTXT;
        private bool _outgoingToCSU;

        #endregion

        #endregion


        #region Constructor

        public SecondSettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            PreviousCommand = new DelegateCommand(Execute_Previous, CanExecute_Previous);
            FinishCommand = new DelegateCommand(Execute_Finish, CanExecute_Finish);

            ChooseInFolderCommand = new DelegateCommand(Execute_ChooseInFolder);
            ChooseOutFolderCommand = new DelegateCommand(Execute_ChooseOutFolder);

            OtherFormatInCommand = new DelegateCommand(Execute_OtherFormatIn);
            OtherFormatOutCommand = new DelegateCommand(Execute_OtherFormatOut);

            LoadDefaultData();

            _eventAggregator.GetEvent<NextSettingsViewEvent>().Subscribe(OnNextSettingsView);
            _eventAggregator.GetEvent<LoggedInEvent>().Subscribe(OnLoggedIn);
        }        

        #endregion


        #region Properties

        public string IncomingFilesLocation
        {
            get { return _incomingFilesLocation; }
            set
            {
                _incomingFilesLocation = value;
                OnPropertyChanged();
                ((DelegateCommand)FinishCommand).RaiseCanExecuteChanged();
            }
        }

        public string OutgoingFilesLocation
        {
            get { return _outgoingFilesLocation; }
            set
            {
                _outgoingFilesLocation = value;
                OnPropertyChanged();
                ((DelegateCommand)FinishCommand).RaiseCanExecuteChanged();
            }
        }

        public string APIKey
        {
            get { return _apiKey; }
            set { _apiKey = value; OnPropertyChanged(); }
        }

        public bool AutomaticSending
        {
            get { return _automaticSending; }
            set { _automaticSending = value; OnPropertyChanged(); }
        }

        public bool AutomaticConversion_OutgoingFiles
        {
            get { return _automaticConversion_OutgoingFiles; }
            set { _automaticConversion_OutgoingFiles = value; OnPropertyChanged(); }
        }

        public bool AutomaticConversion_IncomingFiles
        {
            get { return _automaticConversion_IncomingFiles; }
            set { _automaticConversion_IncomingFiles = value; OnPropertyChanged(); }
        }

        public string CustomIncomingFormat
        {
            get { return _customIncomingFormat; }
            set { _customIncomingFormat = value; OnPropertyChanged(); }
        }

        public string CustomOutgoingFormat
        {
            get { return _customOutgoingFormat; }
            set { _customOutgoingFormat = value; OnPropertyChanged(); }
        }

        public string CustomInFormatTextboxVisibility
        {
            get { return _customInFormatTextboxVisibility; }
            set { _customInFormatTextboxVisibility = value; OnPropertyChanged(); }
        }

        public string CustomOutFormatTextboxVisibility
        {
            get { return _customOutFormatTextboxVisibility; }
            set { _customOutFormatTextboxVisibility = value; OnPropertyChanged(); }
        }

        #region radio buttons

        public bool IncomingToXML
        {
            get { return _incomingToXML; }
            set
            {
                _incomingToXML = value;
                OnPropertyChanged();
                UnselectCustomIncomingFormat();
            }
        }

        public bool IncomingToTXT
        {
            get { return _incomingToTXT; }
            set
            {
                _incomingToTXT = value;
                OnPropertyChanged();
                UnselectCustomIncomingFormat();
            }
        }

        public bool IncomingToCSU
        {
            get { return _incomingToCSU; }
            set
            {
                _incomingToCSU = value;
                OnPropertyChanged();
                UnselectCustomIncomingFormat();
            }
        }

        public bool OutgoingToXML
        {
            get { return _outgoingToXML; }
            set
            {
                _outgoingToXML = value;
                OnPropertyChanged();
                UnselectCustomOutgoingFormat();

            }
        }

        public bool OutgoingToTXT
        {
            get { return _outgoingToTXT; }
            set
            {
                _outgoingToTXT = value;
                OnPropertyChanged();
                UnselectCustomOutgoingFormat();
            }
        }

        public bool OutgoingToCSU
        {
            get { return _outgoingToCSU; }
            set
            {
                _outgoingToCSU = value;
                OnPropertyChanged();
                UnselectCustomOutgoingFormat();
            }
        }

        #endregion

        #endregion

        
        #region Other Methods

        private void LoadDefaultData()
        {
            IncomingFilesLocation = "C:\\SEFLink\\Incoming";
            OutgoingFilesLocation = "C:\\SEFLink\\Outgoing";

            CustomIncomingFormat = "";
            CustomOutgoingFormat = "";

            AutomaticConversion_IncomingFiles = true;
            AutomaticConversion_OutgoingFiles = true;

            CustomInFormatTextboxVisibility = "Collapsed";
            CustomOutFormatTextboxVisibility = "Collapsed";

            IncomingToXML = false;
            IncomingToTXT = false;
            IncomingToCSU = false;

            OutgoingToXML = false;
            OutgoingToTXT = false;
            OutgoingToCSU = false;
        }

        private void SetIncomingFormat(string format)
        {
            IncomingToXML = false;
            IncomingToTXT = false;
            IncomingToCSU = false;

            if (string.IsNullOrEmpty(format) || format == "noFormatSelected")
            {
                return;
            }

            if (format == ".xml")
            {
                IncomingToXML = true;
                return;
            }

            if (format == ".txt")
            {
                IncomingToTXT = true;
                return;
            }

            if (format == ".csu")
            {
                IncomingToCSU = true;
                return;
            }
            
            CustomIncomingFormat = format.Substring(1);
            CustomInFormatTextboxVisibility = "Visible";
        }

        private void SetOutgoingFormat(string format)
        {
            OutgoingToXML = false;
            OutgoingToTXT = false;
            OutgoingToCSU = false;

            if (string.IsNullOrEmpty(format) || format == "noFormatSelected")
            {
                return;
            }

            if (format == ".xml")
            {
                OutgoingToXML = true;
                return;
            }

            if (format == ".txt")
            {
                OutgoingToTXT = true;
                return;
            }

            if (format == ".csu")
            {
                OutgoingToCSU = true;
                return;
            }

            CustomOutgoingFormat = format.Substring(1);
            CustomOutFormatTextboxVisibility = "Visible";

        }

        private bool CanExecute_Previous() => true;

        private void Execute_Previous()
        {
            _eventAggregator.GetEvent<PreviousSettingsViewEvent>().Publish(
                new PreviousSettingsViewEventArgs()
                {
                    AutoConversion_IncomingFiles = AutomaticConversion_IncomingFiles,
                    AutoConversion_OutgoingFiles = AutomaticConversion_OutgoingFiles,
                    AutoSending = AutomaticSending,
                    IncomingFilesLocation = IncomingFilesLocation,
                    OutgoingFilesLocation = OutgoingFilesLocation
                });
        }

        private bool CanExecute_Finish()
        {
            return IncomingFilesLocation.Length != 0 && OutgoingFilesLocation.Length != 0;
        }

        private void Execute_Finish()
        {
            string incomingFormat = DetermineIncomingFormat();
            string outgoingFormat = DetermineOutgoingFormat();

            _eventAggregator.GetEvent<FinishSettingsViewEvent>().Publish(
                new FinishSettingsViewEventArgs()
                {
                    APIKey = APIKey,
                    AutoConversion_IncomingFiles = AutomaticConversion_IncomingFiles,
                    AutoConversion_OutgoingFiles = AutomaticConversion_OutgoingFiles,
                    AutoSending = AutomaticSending,
                    IncomingFilesLocation = IncomingFilesLocation,
                    OutgoingFilesLocation = OutgoingFilesLocation,
                    IncomingFilesFormat = incomingFormat,
                    OutgoingFilesFormat = outgoingFormat
                });
        }

        private string DetermineIncomingFormat()
        {
            string incomingFormat;

            if (AutomaticConversion_IncomingFiles)
            {
                if (IncomingToXML)
                {
                    incomingFormat = ".xml";
                    return incomingFormat;
                }

                if (IncomingToTXT)
                {
                    incomingFormat = ".txt";
                    return incomingFormat;
                }

                if (IncomingToCSU)
                {
                    incomingFormat = ".csu";
                    return incomingFormat;
                }

                if (CustomIncomingFormat != "")
                {
                    incomingFormat = $".{CustomIncomingFormat.ToLower()}";
                    return incomingFormat;
                }


                AutomaticConversion_IncomingFiles = false;
            }

            return "noFormatSelected";
        }

        private string DetermineOutgoingFormat()
        {
            string outgoingFormat;

            if (AutomaticConversion_OutgoingFiles)
            {
                if (OutgoingToXML)
                {
                    outgoingFormat = ".xml";
                    return outgoingFormat;
                }

                if (OutgoingToTXT)
                {
                    outgoingFormat = ".txt";
                    return outgoingFormat;
                }

                if (OutgoingToCSU)
                {
                    outgoingFormat = ".csu";
                    return outgoingFormat;
                }

                if (CustomOutgoingFormat != "")
                {
                    outgoingFormat = $".{CustomOutgoingFormat.ToLower()}";
                    return outgoingFormat;
                }


                AutomaticConversion_OutgoingFiles = false;
            }

            return "noFormatSelected";
        }

        private void Execute_ChooseInFolder()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                IncomingFilesLocation = dialog.SelectedPath;
            }
        }

        private void Execute_ChooseOutFolder()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                OutgoingFilesLocation = dialog.SelectedPath;
            }
        }

        private void Execute_OtherFormatIn()
        {
            IncomingToXML = false;
            IncomingToTXT = false;
            IncomingToCSU = false;

            CustomInFormatTextboxVisibility = "Visible";
        }

        private void Execute_OtherFormatOut()
        {
            OutgoingToXML = false;
            OutgoingToTXT = false;
            OutgoingToCSU = false;

            CustomOutFormatTextboxVisibility = "Visible";
        }

        private void UnselectCustomIncomingFormat()
        {
            CustomInFormatTextboxVisibility = "Collapsed";
            CustomIncomingFormat = "";
        }

        private void UnselectCustomOutgoingFormat()
        {
            CustomOutFormatTextboxVisibility = "Collapsed";
            CustomOutgoingFormat = "";
        }

        private void OnLoggedIn(LoggedInEventArgs args)
        {
            if (args.UserSettings != null) // on existing user
            {
                AutomaticConversion_IncomingFiles = args.UserSettings.AutomaticConversion_IncomingFiles;
                AutomaticConversion_OutgoingFiles = args.UserSettings.AutomaticConversion_OutgoingFiles;

                SetIncomingFormat(args.UserSettings.CustomFormat_Incoming);
                SetOutgoingFormat(args.UserSettings.CustomFormat_Outgoing);
            }
            else // on new user
            {
                LoadDefaultData();
            }
        }

        private void OnNextSettingsView(NextSettingsViewEventArgs args)
        {

            if (args.AutoConversion == false)
            {
                AutomaticConversion_IncomingFiles = false;
                AutomaticConversion_OutgoingFiles = false;
            }
            else
            {
                if (!AutomaticConversion_IncomingFiles && !AutomaticConversion_OutgoingFiles)
                {
                    AutomaticConversion_IncomingFiles = true;
                    AutomaticConversion_OutgoingFiles = true;
                }
            }

            AutomaticSending = args.AutoSending;

            APIKey = args.APIKey;

            IncomingFilesLocation = args.IncomingFilesLocation;
            OutgoingFilesLocation = args.OutgoingFilesLocation;
        }

        #endregion
    }
}
