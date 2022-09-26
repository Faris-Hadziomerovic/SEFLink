using Prism.Events;
using SEFLink.UI.HCI.Models;
using SEFLink.UI.HCI.ViewModels;

namespace SEFLink.UI.HCI.Helpers
{
    public class OrderItemHelper
    {
        #region Helper Methods

        public static OrderItemViewModel CreateOrderItemViewModel(IEventAggregator eventAggregator, OrderItem item)
        {            
            return new OrderItemViewModel(eventAggregator, item.Id, item.Image, item.Name, item.Price);
        }

        #endregion
    }

    public enum DocumentInfoType
    {
        Incoming = 1, Outgoing = 2
    }
}
