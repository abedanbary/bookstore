using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace idefny.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationPropertyToWaitlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Waitlists_BookId",
                table: "Waitlists",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Waitlists_Books_BookId",
                table: "Waitlists",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Waitlists_Books_BookId",
                table: "Waitlists");

            migrationBuilder.DropIndex(
                name: "IX_Waitlists_BookId",
                table: "Waitlists");
        }
    }
}
