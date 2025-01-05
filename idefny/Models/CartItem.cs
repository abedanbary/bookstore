namespace idefny.Models
{
    public class CartItem
    {
        public int Id { get; set; } // Primary Key
        public int BookId { get; set; } // Foreign Key to the Book model
        public Book Book { get; set; } // Navigation property to Book
        public int Quantity { get; set; } // Number of copies of the book in the cart
        public decimal Price { get; set; } // Price of the book at the time it was added to the cart
        public bool IsBuying2 { get; set; }

        public int CartId { get; set; } // Foreign Key to the Cart
        public Cart Cart { get; set; } // Navigation property to Cart
    }
}
