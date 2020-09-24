using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreMvc.Migrations
{
    public partial class InitialListOfData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Office Email", "Sex", "FullName", "Salary" },
                values: new object[] { 1, "wonkulahp@yahoo.com", "Female", "Precious K Wonkulah", 30000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Office Email", "Sex", "FullName", "Salary" },
                values: new object[] { 2, "john@gmail.com", "Male", "John Brown", 6000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Office Email", "Sex", "FullName", "Salary" },
                values: new object[] { 3, "dacious@hotmail.com", "Female", "Dacious R Wonkulah", 250m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);
        }
    }
}
