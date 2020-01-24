using Microsoft.EntityFrameworkCore.Migrations;

namespace Patronage2020.Persistence.Migrations
{
    public partial class UseAutoMapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "WritingFiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "WritingFiles");
        }
    }
}
