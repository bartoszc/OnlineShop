using System;
using System.Collections.Generic;

namespace OnlineShop.Backend.Models
{
    public partial class Cards
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public string NameOnCard { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Cvv { get; set; }
    }
}
