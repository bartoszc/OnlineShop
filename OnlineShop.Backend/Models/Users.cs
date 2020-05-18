using System;
using System.Collections.Generic;

namespace OnlineShop.Backend.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Pass { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
