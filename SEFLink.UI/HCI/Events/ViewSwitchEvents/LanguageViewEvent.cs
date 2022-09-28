using Prism.Events;

namespace SEFLink.UI.HCI.Events
{
    public class LanguageViewEvent : PubSubEvent<LanguageViewEventArgs>
    {
    }

    public class LanguageViewEventArgs
    {
        public bool OriginIsPayment { get; set; } = false;
    }
}
