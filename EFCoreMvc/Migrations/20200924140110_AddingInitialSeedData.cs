using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreMvc.Migrations
{
    public partial class AddingInitialSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Department", "Office Email", "Sex", "FullName", "Salary" },
                values: new object[] { 1, "Accountant", "wonkulahp@yahoo.com", "Female", "Precious K Wonkulah", 7000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Department", "Office Email", "Sex", "FullName", "Salary" },
                values: new object[] { 2, "HR", "dacious@outlook.com", "Female", "Dacious R Wonkulah", 5000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Department", "Office Email", "Sex", "FullName", "Salary" },
                values: new object[] { 3, "IT", "john@hotmail.com", "Male", "John Brown", 5000m });
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
