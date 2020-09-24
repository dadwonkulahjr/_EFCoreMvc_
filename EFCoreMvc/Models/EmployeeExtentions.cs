using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.Models
{
    public static class EmployeeExtentions
    {
        public static void SeedInitialData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(e =>
            {
                e.HasData(
                    new Employee()
                    {
                        EmployeeId = 1,
                        Name = "Precious K Wonkulah",
                        Email = "wonkulahp@yahoo.com",
                        Gender = Gender.Female,
                        Department = Department.Accountant,
                        Salary = 7000
                    },
                     new Employee()
                     {
                         EmployeeId = 2,
                         Name = "Dacious R Wonkulah",
                         Email = "dacious@outlook.com",
                         Gender = Gender.Female,
                         Department = Department.HR,
                         Salary = 5000
                     },
                       new Employee()
                       {
                           EmployeeId = 3,
                           Name = "John Brown",
                           Email = "john@hotmail.com",
                           Gender = Gender.Male,
                           Department = Department.IT,
                           Salary = 5000
                       }
                    );
            });
        }
    }
}
