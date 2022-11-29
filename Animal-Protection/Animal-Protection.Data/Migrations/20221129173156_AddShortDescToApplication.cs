using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animal_Protection.Data.Migrations
{
    public partial class AddShortDescToApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDesciption",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDesciption",
                table: "Applications");
        }
    }
}
