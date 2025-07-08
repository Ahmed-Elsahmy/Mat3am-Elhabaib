using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mat3am_Elhabaib.Migrations
{
    /// <inheritdoc />
    public partial class Editdsss10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delevird",
                table: "orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delevird",
                table: "orders");
        }
    }
}
