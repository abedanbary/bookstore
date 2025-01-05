using System;
namespace idefny.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }  // User who gave the review
        public int Rating { get; set; } // Rating out of 5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public Book Book { get; set; }  // Navigation to the Book
    }


}

