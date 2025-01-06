using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace idefny.Migrations
{
    /// <inheritdoc />
    public partial class AddBooksSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Age", "Author", "AvailableForBorrow", "DatePublished", "DiscountEndDate", "DiscountPercentage2", "DiscountStartDate", "Genre", "ImageUrl", "IsAvailableForBorrow", "Name", "NumberOfCopies", "PdfUrl", "Price" },
                values: new object[,]
                {
                    { -10, 17, "Margaret Mitchell", 3, new DateTime(1936, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Romance", "/images/book10.jpg", true, "Gone with the Wind", 7, "/pdfs/book10.pdf", 14.99m },
                    { -9, 14, "Nicholas Sparks", 5, new DateTime(1996, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Romance", "/images/book9.jpg", true, "The Notebook", 10, "/pdfs/book9.pdf", 9.99m },
                    { -8, 15, "Emily Brontë", 2, new DateTime(1847, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Romance", "/images/book8.jpg", true, "Wuthering Heights", 5, "/pdfs/book8.pdf", 11.49m },
                    { -7, 15, "Charlotte Brontë", 3, new DateTime(1847, 10, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Romance", "/images/book7.jpg", true, "Jane Eyre", 6, "/pdfs/book7.pdf", 12.99m },
                    { -6, 16, "Jane Austen", 4, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Romance", "/images/book6.jpg", true, "Pride and Prejudice", 8, "/pdfs/book6.pdf", 10.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -6);
        }
    }
}
