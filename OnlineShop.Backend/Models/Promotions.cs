using System;
using System.Collections.Generic;

namespace OnlineShop.Backend.Models
{
    public partial class Promotions
    {
        public Promotions()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int PromoId { get; set; }
        public double? DiscountValue { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
