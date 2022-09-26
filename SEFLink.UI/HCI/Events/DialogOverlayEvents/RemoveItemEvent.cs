using Prism.Events;
using SEFLink.UI.HCI.Models;

namespace SEFLink.UI.HCI.Events
{
    public class RemoveItemEvent : PubSubEvent<RemoveItemEventArgs> { }

    public class RemoveItemEventArgs
    {
        public OrderItem OrderItem { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
