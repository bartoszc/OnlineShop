using System;
using System.Collections.Generic;

namespace OnlineShop.Backend.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public byte[] ImageColumn { get; set; }
        public string ProductDescription { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
