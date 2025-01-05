using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace idefny.Migrations
{
    /// <inheritdoc />
    public partial class AddWaitListDateToBorrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "WaitListDate",
                table: "Borrows",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WaitListDate",
                table: "Borrows");
        }
    }
}
