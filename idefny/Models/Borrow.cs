using System;
using System.ComponentModel.DataAnnotations;

namespace idefny.Models
{
    public class Borrow
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int BookId { get; set; } // Foreign Key for Book

        [Required]
        public string UserId { get; set; } // User who borrowed the book

        [Required]
        public DateTime BorrowDate { get; set; } = DateTime.UtcNow; // Date when the book was borrowed

        [Required]
        public DateTime ReturnDate { get; set; } // Expected return date

        [Required]
        public bool IsReturned { get; set; } = false; // Status of return
        public DateTime? WaitListDate { get; set; }
        // Navigation Properties
        public Book Book { get; set; } // Reference to the book
       
    }
}
