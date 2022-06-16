using Prism.Commands;
using Prism.Events;
using SEFLink.Model;
using SEFLink.UI.Data;
using SEFLink.UI.Events;
using System;
using System.Windows;
using System.Windows.Input;

namespace SEFLink.UI.ViewModels
{
    public class SettingsViewModel : Observable
    {
        #region Fields

        private User _user;
        
        private BaseTitleBarViewModel _baseTitleBarViewModel;

        private FirstSettingsViewModel _firstSettingsViewModel;
        private SecondSettingsViewModel _secondSettingsViewModel;

        private object _currentSettingsViewModel;
        private IEventAggregator _eventAggregator;
        private IUserDataService _userDataService;

        private string _viewTitle;
        private readonly string _firstTitle = "Settings 1/2";
        private readonly string _secondTitle = "Settings 2/2";

        #endregion


        #region Constructor

        public SettingsViewModel (BaseTitleBarViewModel baseTitleBarViewModel,                                 
                                  FirstSettingsViewModel firstSettingsViewModel,
                                  SecondSettingsViewModel secondSettingsViewModel,
                                  IUserDataService userDataService,
                                  IEventAggregator eventAggregator)
        {

            BaseTitleBarViewModel = baseTitleBarViewModel;

            FirstSettingsViewModel = firstSettingsViewModel;
            SecondSettingsViewModel = secondSettingsViewModel;

            CurrentSettingsViewModel = firstSettingsViewModel;

            ViewTitle = _firstTitle;

            _eventAggregator = eventAggregator;
            _userDataService = userDataService;

            _eventAggregator.GetEvent<LoggedInEvent>().Subscribe(OnLoggedIn);
            _eventAggregator.GetEvent<NextSettingsViewEvent>().Subscribe(OnNextSettingsView);
            _eventAggregator.GetEvent<PreviousSettingsViewEvent>().Subscribe(OnPreviousSettingsView);
            _eventAggregator.GetEvent<FinishSettingsViewEvent>().Subscribe(OnFinishSettingsView);
        }

        #endregion


        #region Properties

        public User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        public object CurrentSettingsViewModel
        {
            get { return _currentSettingsViewModel; }
            set { _currentSettingsViewModel = value; OnPropertyChanged(); }
        }

        public BaseTitleBarViewModel BaseTitleBarViewModel
        {
            get { return _baseTitleBarViewModel; }
            set { _baseTitleBarViewModel = value; OnPropertyChanged(); }
        }
     
        public FirstSettingsViewModel FirstSettingsViewModel
        {
            get { return _firstSettingsViewModel; }
            set { _firstSettingsViewModel = value; OnPropertyChanged(); }
        }

        public SecondSettingsViewModel SecondSettingsViewModel
        {
            get { return _secondSettingsViewModel; }
            set { _secondSettingsViewModel = value; OnPropertyChanged(); }
        }

        public string ViewTitle
        {
            get { return _viewTitle; }
            set { _viewTitle = value; OnPropertyChanged(); }
        }

        #endregion


        #region Other Methods

        private void OnLoggedIn(LoggedInEventArgs args)
        {
            User = _userDataService.GetUserById(args.Id);                        

            CurrentSettingsViewModel = FirstSettingsViewModel;
        }

        private void OnNextSettingsView(NextSettingsViewEventArgs args)
        {
            CurrentSettingsViewModel = SecondSettingsViewModel;
            ViewTitle = _secondTitle;

            //FirstSettingsViewData(args);
        }

        private void OnPreviousSettingsView(PreviousSettingsViewEventArgs args)
        {
            CurrentSettingsViewModel = FirstSettingsViewModel;
            ViewTitle = _firstTitle;

            //SecondSettingsViewData(args);
        }

        private void OnFinishSettingsView(FinishSettingsViewEventArgs args)
        {
            User.UserSettings = new UserSettings
            {
                API_Key = args.APIKey,
                IncomingFilesLocation = args.IncomingFilesLocation,
                OutgoingFilesLocation = args.OutgoingFilesLocation,
                AutomaticSending = args.AutoSending,
                AutomaticConversion_IncomingFiles = args.AutoConversion_IncomingFiles,
                AutomaticConversion_OutgoingFiles = args.AutoConversion_OutgoingFiles,
                CustomFormat_Incoming = args.IncomingFilesFormat,
                CustomFormat_Outgoing = args.OutgoingFilesFormat
            };

            //FinalSettingsData(args);
        }
       

        



        private static void FirstSettingsViewData(NextSettingsViewEventArgs args)
        {
            string DataTransmission = "Data: \n";

            DataTransmission += $"Incoming location: [{args.IncomingFilesLocation}]\n";
            DataTransmission += $"Outgoing location: [{args.OutgoingFilesLocation}]\n";
            DataTransmission += $"Automatic sending: [{args.AutoSending.ToString()}]\n";
            DataTransmission += $"Automatic conversion: [{args.AutoConversion}]\n";

            System.Windows.MessageBox.Show(DataTransmission,
                                           "Next screen successful!",
                                           MessageBoxButton.OK,
                                           MessageBoxImage.Information);
        }
        
        private static void SecondSettingsViewData(PreviousSettingsViewEventArgs args)
        {
            string DataTransmission = "Data: \n";

            DataTransmission += $"Incoming location: [{args.IncomingFilesLocation}]\n";
            DataTransmission += $"Outgoing location: [{args.OutgoingFilesLocation}]\n";

            DataTransmission += $"Automatic conversion of incoming files: [{args.AutoConversion_IncomingFiles}]\n";
            DataTransmission += $"Automatic conversion of outgoing files: [{args.AutoConversion_OutgoingFiles}]\n";

            System.Windows.MessageBox.Show(DataTransmission,
                                           "Previous screen successful!",
                                           MessageBoxButton.OK,
                                           MessageBoxImage.Information);
        }

        private static void FinalSettingsData(FinishSettingsViewEventArgs args)
        {
            string DataTransmission = "Data: \n";

            DataTransmission += $"Incoming location: [{args.IncomingFilesLocation}]\n";
            DataTransmission += $"Outgoing location: [{args.OutgoingFilesLocation}]\n";
            DataTransmission += $"Automatic sending: [{args.AutoSending}]\n";
            DataTransmission += $"Automatic conversion of incoming files: [{args.AutoConversion_IncomingFiles}]\n";
            DataTransmission += $"Selected format for incoming files: [{args.IncomingFilesFormat}]\n";
            DataTransmission += $"Automatic conversion of outgoing files: [{args.AutoConversion_OutgoingFiles}]\n";
            DataTransmission += $"Selected format for outgoing files: [{args.OutgoingFilesFormat}]\n";

            System.Windows.MessageBox.Show(DataTransmission,
                                           "Finished screen successful!",
                                           MessageBoxButton.OK,
                                           MessageBoxImage.Information);
        }

        #endregion
    }
}
