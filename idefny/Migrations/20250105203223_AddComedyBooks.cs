using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace idefny.Migrations
{
    /// <inheritdoc />
    public partial class AddComedyBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Age", "Author", "AvailableForBorrow", "DatePublished", "DiscountEndDate", "DiscountPercentage2", "DiscountStartDate", "Genre", "ImageUrl", "IsAvailableForBorrow", "Name", "NumberOfCopies", "PdfUrl", "Price" },
                values: new object[,]
                {
                    { -5, 10, "Mark Twain", 3, new DateTime(1876, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Comedy", "/images/book5.jpg", true, "The Adventures of Tom Sawyer", 10, "/pdfs/book5.pdf", 7.99m },
                    { -4, 16, "P.G. Wodehouse", 3, new DateTime(1934, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Comedy", "/images/book4.jpg", true, "P.G. Wodehouse Collection (e.g., Jeeves and Wooster)", 6, "/pdfs/book4.pdf", 11.99m },
                    { -3, 15, "Miguel de Cervantes", 3, new DateTime(1605, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Comedy", "/images/book3.jpg", true, "Don Quixote", 3, "/pdfs/book3.pdf", 13.99m },
                    { -2, 12, "Jerome K. Jerome", 3, new DateTime(1889, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Comedy", "/images/book2.jpg", true, "Three Men in a Boat", 7, "/pdfs/book2.pdf", 8.99m },
                    { -1, 14, "Oscar Wilde", 3, new DateTime(1895, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Comedy", "/images/book1.jpg", true, "The Importance of Being Earnest", 5, "/pdfs/book1.pdf", 9.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
