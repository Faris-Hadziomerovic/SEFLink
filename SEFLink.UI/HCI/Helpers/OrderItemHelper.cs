using Prism.Events;
using SEFLink.UI.HCI.Models;
using SEFLink.UI.HCI.ViewModels;

namespace SEFLink.UI.HCI.Helpers
{
    public class OrderItemHelper
    {
        #region Helper Methods

        public static OrderItemViewModel CreateOrderItemViewModel(IEventAggregator eventAggregator, OrderItem item, bool isCartItem = true)
        {            
            return new OrderItemViewModel(eventAggregator, item.Id, item.Image, item.Name, item.Description, item.Price, isCartItem);
        }

        #endregion
    }
}
