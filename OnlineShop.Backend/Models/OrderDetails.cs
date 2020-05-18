using System;
using System.Collections.Generic;

namespace OnlineShop.Backend.Models
{
    public partial class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
      
        public double OrderCost { get; set; }
        public int Quantity { get; set; }
        public int PromoId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
        public virtual Promotions Promo { get; set; }
    }
}
