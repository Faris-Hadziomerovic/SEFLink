using Prism.Events;

namespace SEFLink.UI.Events
{
    public class PreviousSettingsViewEvent : PubSubEvent<PreviousSettingsViewEventArgs> { }

    public class PreviousSettingsViewEventArgs
    {
        public bool AutoConversion_IncomingFiles { get; set; }
        public bool AutoConversion_OutgoingFiles { get; set; }
        public bool AutoSending { get; set; }
        public string IncomingFilesLocation { get; set; }
        public string OutgoingFilesLocation { get; set; }
    }
}
