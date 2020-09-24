using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.Models
{
    public class EmployeeImplementation : IEmployeeRepo
    {
        private List<Employee> _employees;
        public EmployeeImplementation()
        {
            _employees = new List<Employee>()
            {
                new Employee()
                {
                    EmployeeId = 1,
                    Name = "Mike Books",
                    Email = "mike@gmail.com",
                    Gender =  Gender.Male,
                    Salary = 5000m
                },
                  new Employee()
                {
                    EmployeeId = 2,
                    Name = "Mary James",
                    Email = "mike@yahoo.com",
                    Gender = Gender.Female,
                    Salary = 6500
                },

                new Employee()
                {
                    EmployeeId = 3,
                    Name = "Sam Johnson",
                    Email = "sam@hotmail.com",
                    Gender = Gender.Male,
                    Salary = 8000
                },
                  new Employee()
                {
                    EmployeeId = 4,
                    Name = "Princess Walker",
                    Email = "princess@outlook.com",
                    Gender =Gender.Female,
                    Salary = 7000
                },
            };
        }
        public Employee Add(Employee employee)
        {
            employee.EmployeeId = _employees.Max(x => x.EmployeeId) + 1;
            _employees.Add(employee);
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = _employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(x => x.EmployeeId == id);
        }

        public IEnumerable<Employee> GetListOfEmployees()
        {
            return _employees;
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            Employee employee = _employees.FirstOrDefault(x => x.EmployeeId == updatedEmployee.EmployeeId);
            if (updatedEmployee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.Gender = updatedEmployee.Gender;
                employee.Salary = updatedEmployee.Salary;
            }
            return employee;
        }
    }
}
