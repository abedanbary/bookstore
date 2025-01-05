using System;
namespace idefny.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } // To associate the order with a specific user
        public DateTime OrderDate { get; set; } // Date when the order was placed
        public ICollection<OrderItem> OrderItems { get; set; } // Collection of borrowed books
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } // Navigation property
        public int BookId { get; set; }
        public Book Book { get; set; } // Navigation property
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
    }

}

