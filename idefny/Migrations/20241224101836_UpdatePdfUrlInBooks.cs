using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace idefny.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePdfUrlInBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "Books",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "Books");
        }
    }
}
