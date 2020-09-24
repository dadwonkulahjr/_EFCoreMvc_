using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(b =>
            {
                b.HasData(
                    ListOfInitialEmployees()
                    );

            });
        }

        private IList<Employee> ListOfInitialEmployees()
        {
            List<Employee> listOfEmployees = new List<Employee>()
           {
               new Employee()
               {
                   EmployeeId = 1,
                   Name = "Precious K Wonkulah",
                   Email = "wonkulahp@yahoo.com",
                   Gender = Gender.Female,
                   Salary = 30000,
                   Departmen = Department.Accountant
               },
               new Employee()
               {
                   EmployeeId = 2,
                   Name = "John Brown",
                   Email = "john@gmail.com",
                   Gender = Gender.Male,
                   Salary = 6000,
                   Departmen = Department.IT
               },
               new Employee()
               {
                   EmployeeId = 3,
                   Name = "Dacious R Wonkulah",
                   Email = "dacious@hotmail.com",
                   Gender = Gender.Female,
                   Salary = 250,
                   Departmen = Department.Payroll
               }
           };

            return listOfEmployees;
        }
    }
}
