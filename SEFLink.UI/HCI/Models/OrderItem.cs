using System;

namespace SEFLink.UI.HCI.Models
{
    public class OrderItem
    {
        public OrderItem()
        {
            var random = new Random();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
