using System;
using System.ComponentModel.DataAnnotations;

namespace idefny.Models
{
    public class BorrowedBook
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int BookId { get; set; } // Foreign Key for Book

        [Required]
        public string UserId { get; set; } // Foreign Key for User (assuming you have a User model)

        [Required]
        public DateTime BorrowDate { get; set; } // Date when the book was borrowed

        [Required]
        [Range(1, 30)] // User can borrow the book for 1 to 30 days
        public int DaysToBorrow { get; set; } // Number of days the book is borrowed for

        public Book Book { get; set; } // Navigation property to the Book
        public Users User { get; set; } // Navigation property to the User (assuming you have a User model)

        // Calculate the return date for the borrowed book
        public DateTime ReturnDate2 { get; set; }
    }
}
