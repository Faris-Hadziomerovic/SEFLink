using Prism.Events;

namespace SEFLink.UI.Events
{
    public class NextSettingsViewEvent : PubSubEvent<NextSettingsViewEventArgs> { }

    public class NextSettingsViewEventArgs
    {
        public string APIKey { get; set; }
        public string IncomingFilesLocation { get; set; }
        public string OutgoingFilesLocation { get; set; }
        public bool AutoConversion { get; set; }
        public bool AutoSending { get; set; }
    }
}
