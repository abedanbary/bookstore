using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using idefny.Models;
using Microsoft.EntityFrameworkCore;

namespace idefny.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Waitlist> Waitlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Cart)
                .WithMany(ci => ci.CartItems)
                .HasForeignKey(c => c.CartId);

            // UserBook relationships
            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.User)
                .WithMany()
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.Book)
                .WithMany()
                .HasForeignKey(ub => ub.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships for BorrowedBook
            modelBuilder.Entity<BorrowedBook>()
                .HasOne(bb => bb.Book)
                .WithMany()
                .HasForeignKey(bb => bb.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BorrowedBook>()
                .HasOne(bb => bb.User)
                .WithMany()
                .HasForeignKey(bb => bb.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships for Waitlist
            modelBuilder.Entity<Waitlist>()
                .HasOne(w => w.Book)
                .WithMany()
                .HasForeignKey(w => w.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Waitlist>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>().HasData(
             new Book
             {
                 Id = -1,
                 Name = "The Importance of Being Earnest",
                 Author = "Oscar Wilde",
                 Age = 14,
                 Genre = "Comedy",
                 NumberOfCopies = 5,
                 AvailableForBorrow = 3,
                 IsAvailableForBorrow = true,
                 DatePublished = new DateTime(1895, 2, 14, 0, 0, 0, DateTimeKind.Utc), // تعيين التوقيت كـ UTC
                 Price = 9.99m,
                 ImageUrl = "/images/book1.jpg",
                 PdfUrl = "/pdfs/book1.pdf",
                 DiscountPercentage2 = null,
                 DiscountStartDate = null,
                 DiscountEndDate = null
             },
             new Book
             {
                 Id = -2,
                 Name = "Three Men in a Boat",
                 Author = "Jerome K. Jerome",
                 Age = 12,
                 Genre = "Comedy",
                 NumberOfCopies = 7,
                 AvailableForBorrow = 3,
                 IsAvailableForBorrow = true,
                 DatePublished = new DateTime(1889, 5, 5, 0, 0, 0, DateTimeKind.Utc), // تعيين التوقيت كـ UTC
                 Price = 8.99m,
                 ImageUrl = "/images/book2.jpg",
                 PdfUrl = "/pdfs/book2.pdf",
                 DiscountPercentage2 = null,
                 DiscountStartDate = null,
                 DiscountEndDate = null
             },
             new Book
             {
                 Id = -3,
                 Name = "Don Quixote",
                 Author = "Miguel de Cervantes",
                 Age = 15,
                 Genre = "Comedy",
                 NumberOfCopies = 3,
                 AvailableForBorrow = 3,
                 IsAvailableForBorrow = true,
                 DatePublished = new DateTime(1605, 1, 16, 0, 0, 0, DateTimeKind.Utc), // تعيين التوقيت كـ UTC
                 Price = 13.99m,
                 ImageUrl = "/images/book3.jpg",
                 PdfUrl = "/pdfs/book3.pdf",
                 DiscountPercentage2 = null,
                 DiscountStartDate = null,
                 DiscountEndDate = null
             },
             new Book
             {
                 Id = -4,
                 Name = "P.G. Wodehouse Collection (e.g., Jeeves and Wooster)",
                 Author = "P.G. Wodehouse",
                 Age = 16,
                 Genre = "Comedy",
                 NumberOfCopies = 6,
                 AvailableForBorrow = 3,
                 IsAvailableForBorrow = true,
                 DatePublished = new DateTime(1934, 4, 15, 0, 0, 0, DateTimeKind.Utc), // تعيين التوقيت كـ UTC
                 Price = 11.99m,
                 ImageUrl = "/images/book4.jpg",
                 PdfUrl = "/pdfs/book4.pdf",
                 DiscountPercentage2 = null,
                 DiscountStartDate = null,
                 DiscountEndDate = null
             },
             new Book
             {
                 Id = -5,
                 Name = "The Adventures of Tom Sawyer",
                 Author = "Mark Twain",
                 Age = 10,
                 Genre = "Comedy",
                 NumberOfCopies = 10,
                 AvailableForBorrow = 3,
                 IsAvailableForBorrow = true,
                 DatePublished = new DateTime(1876, 6, 1, 0, 0, 0, DateTimeKind.Utc), // تعيين التوقيت كـ UTC
                 Price = 7.99m,
                 ImageUrl = "/images/book5.jpg",
                 PdfUrl = "/pdfs/book5.pdf",
                 DiscountPercentage2 = null,
                 DiscountStartDate = null,
                 DiscountEndDate = null
             }
         );




        }
    }
}
