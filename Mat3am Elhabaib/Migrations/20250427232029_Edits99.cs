using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mat3am_Elhabaib.Migrations
{
    /// <inheritdoc />
    public partial class Edits99 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "reservations");
        }
    }
}
