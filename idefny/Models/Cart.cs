using System;
using System.Collections.Generic;

namespace idefny.Models
{
    public class Cart
    {
        public int Id { get; set; } // Primary Key
        public string UserId { get; set; } // User reference (if using authentication)

        public ICollection<CartItem> CartItems { get; set; } // Navigation property for the items in the cart
    }
}

