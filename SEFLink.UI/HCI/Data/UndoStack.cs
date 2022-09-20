using Prism.Events;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.Model;
using System.Collections.Generic;

namespace SEFLink.UI.HCI.Data
{
    public class UndoStack
    {
        private List<OrderItem> _orderItems;
        private IEventAggregator _eventAggregator;



        public UndoStack(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _orderItems = new List<OrderItem>();

            _eventAggregator.GetEvent<RemoveItemEvent>().Subscribe(OnRemoveItem);
            _eventAggregator.GetEvent<AddItemEvent>().Subscribe(OnAddItem);
        }



        public List<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; }
        }



        public void OnRemoveItem(RemoveItemEventArgs args)
        {
            //_orderItems.Add(args.item);
        }
        
        public void OnAddItem(AddItemEventArgs args)
        {
            _orderItems.Clear();
        }
    }
}
