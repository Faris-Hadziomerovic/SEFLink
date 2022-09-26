using Prism.Events;
using SEFLink.UI.HCI.Events;
using SEFLink.UI.HCI.Models;
using System;
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

            _eventAggregator.GetEvent<RemoveItemConfirmedEvent>().Subscribe(OnRemoveItem);
            _eventAggregator.GetEvent<AddItemEvent>().Subscribe(OnAddItem);
            _eventAggregator.GetEvent<UndoRemoveItemEvent>().Subscribe(OnUndo);
        }


        public List<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; }
        }


        private void OnUndo(UndoRemoveItemEventArgs args)
        {
            if (OrderItems.Count == 0) return;

            _eventAggregator.GetEvent<AddItemEvent>().Publish(new AddItemEventArgs
            {
                UndoExecuted = true,
                OrderItem = OrderItems[OrderItems.Count - 1]
            });

            OrderItems.RemoveAt(OrderItems.Count - 1);
        }

        public void OnRemoveItem(RemoveItemConfirmedEventArgs args)
        {
            OrderItems.Add(args.OrderItem);
        }
        
        public void OnAddItem(AddItemEventArgs args)
        {
            if (args.UndoExecuted) return;

            _orderItems.Clear();
        }
    }
}
