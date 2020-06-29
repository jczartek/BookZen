using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RenameDateOfPublicationToYearOfPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPublication",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "YearOfPublication",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfPublication",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "DateOfPublication",
                table: "Books",
                type: "int",
                nullable: true);
        }
    }
}
