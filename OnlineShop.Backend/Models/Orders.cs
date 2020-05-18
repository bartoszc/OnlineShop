using System;
using System.Collections.Generic;

namespace OnlineShop.Backend.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingMethod { get; set; }
        public string OrderStatus { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual Users User { get; set; }
    }
}
