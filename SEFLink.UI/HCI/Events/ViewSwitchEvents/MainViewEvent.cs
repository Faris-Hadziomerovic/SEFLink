using Prism.Events;

namespace SEFLink.UI.HCI.Events
{
    public class MenuViewEvent : PubSubEvent<MenuViewEventArgs> { }

    public class MenuViewEventArgs
    {
        public bool OriginIsPaymentView { get; set; }
    }
}
