using Prism.Events;

namespace SEFLink.UI.HCI.Events
{
    public class ChangeLanguageEvent : PubSubEvent<ChangeLanguageEventArgs> { }

    public class ChangeLanguageEventArgs
    {
        public string Language { get; set; } = "English";
    }
}