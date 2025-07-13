using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mat3am_Elhabaib.Migrations
{
    /// <inheritdoc />
    public partial class MakeImageWithUploadFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imageurl",
                table: "items",
                newName: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "items",
                newName: "Imageurl");
        }
    }
}
