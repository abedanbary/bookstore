using System;
using System.ComponentModel.DataAnnotations;

namespace idefny.Models
{
    public class Waitlist
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int BookId { get; set; } // Foreign Key for Book

        [Required]
        public string UserId { get; set; } // Foreign Key for User (assuming you have a User model)

        [Required]
        public DateTime DateJoined { get; set; } // Date when the user joined the waitlist
        public DateTime? Dateborrowbook { get; set; } // Date when the user joined the waitlist

        public Book Book { get; set; } // Navigation property to the Book
        public Users User { get; set; } // Navigation property to the User (assuming you have a User model)
    }
}
