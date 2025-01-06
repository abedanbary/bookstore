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
            modelBuilder.Entity<Book>().HasData(
    new Book
    {
        Id = -6,
        Name = "Pride and Prejudice",
        Author = "Jane Austen",
        Age = 16,
        Genre = "Romance",
        NumberOfCopies = 8,
        AvailableForBorrow = 4,
        IsAvailableForBorrow = true,
        DatePublished = new DateTime(1813, 1, 28, 0, 0, 0, DateTimeKind.Utc),
        Price = 10.99m,
        ImageUrl = "/images/book6.jpg",
        PdfUrl = "/pdfs/book6.pdf",
        DiscountPercentage2 = null,
        DiscountStartDate = null,
        DiscountEndDate = null
    },
    new Book
    {
        Id = -7,
        Name = "Jane Eyre",
        Author = "Charlotte Brontë",
        Age = 15,
        Genre = "Romance",
        NumberOfCopies = 6,
        AvailableForBorrow = 3,
        IsAvailableForBorrow = true,
        DatePublished = new DateTime(1847, 10, 16, 0, 0, 0, DateTimeKind.Utc),
        Price = 12.99m,
        ImageUrl = "/images/book7.jpg",
        PdfUrl = "/pdfs/book7.pdf",
        DiscountPercentage2 = null,
        DiscountStartDate = null,
        DiscountEndDate = null
    },
    new Book
    {
        Id = -8,
        Name = "Wuthering Heights",
        Author = "Emily Brontë",
        Age = 15,
        Genre = "Romance",
        NumberOfCopies = 5,
        AvailableForBorrow = 2,
        IsAvailableForBorrow = true,
        DatePublished = new DateTime(1847, 12, 17, 0, 0, 0, DateTimeKind.Utc),
        Price = 11.49m,
        ImageUrl = "/images/book8.jpg",
        PdfUrl = "/pdfs/book8.pdf",
        DiscountPercentage2 = null,
        DiscountStartDate = null,
        DiscountEndDate = null
    },
    new Book
    {
        Id = -9,
        Name = "The Notebook",
        Author = "Nicholas Sparks",
        Age = 14,
        Genre = "Romance",
        NumberOfCopies = 10,
        AvailableForBorrow = 5,
        IsAvailableForBorrow = true,
        DatePublished = new DateTime(1996, 10, 1, 0, 0, 0, DateTimeKind.Utc),
        Price = 9.99m,
        ImageUrl = "/images/book9.jpg",
        PdfUrl = "/pdfs/book9.pdf",
        DiscountPercentage2 = null,
        DiscountStartDate = null,
        DiscountEndDate = null
    },
    new Book
    {
        Id = -10,
        Name = "Gone with the Wind",
        Author = "Margaret Mitchell",
        Age = 17,
        Genre = "Romance",
        NumberOfCopies = 7,
        AvailableForBorrow = 3,
        IsAvailableForBorrow = true,
        DatePublished = new DateTime(1936, 6, 30, 0, 0, 0, DateTimeKind.Utc),
        Price = 14.99m,
        ImageUrl = "/images/book10.jpg",
        PdfUrl = "/pdfs/book10.pdf",
        DiscountPercentage2 = null,
        DiscountStartDate = null,
        DiscountEndDate = null
    }
);




        }
    }
}
