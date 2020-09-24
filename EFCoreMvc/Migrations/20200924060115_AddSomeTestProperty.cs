using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreMvc.Migrations
{
    public partial class AddSomeTestProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SomeTestProperty",
                table: "Employees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SomeTestProperty",
                table: "Employees");
        }
    }
}
