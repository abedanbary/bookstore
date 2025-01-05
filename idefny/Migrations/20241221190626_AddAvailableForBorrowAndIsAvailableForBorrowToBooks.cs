using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace idefny.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailableForBorrowAndIsAvailableForBorrowToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableForBorrow",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableForBorrow",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableForBorrow",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsAvailableForBorrow",
                table: "Books");
        }
    }
}
