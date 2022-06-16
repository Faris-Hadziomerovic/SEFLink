using Prism.Events;

namespace SEFLink.UI.Events
{
    public class FinishSettingsViewEvent : PubSubEvent<FinishSettingsViewEventArgs> { }

    public class FinishSettingsViewEventArgs
    {
        public string APIKey { get; set; }
        public bool AutoConversion_IncomingFiles { get; set; }
        public bool AutoConversion_OutgoingFiles { get; set; }
        public bool AutoSending { get; set; }
        public string IncomingFilesLocation { get; set; }
        public string OutgoingFilesLocation { get; set; }
        public string IncomingFilesFormat { get; set; }
        public string OutgoingFilesFormat { get; set; }
    }
}
