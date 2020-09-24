using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.Models
{
    public class SQLRepository : IEmployeeRepo
    {
        private ApplicationDbContext _dbContext;

        public SQLRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Employee Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = _dbContext.Employees.Find(id);
            if(employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return _dbContext.Employees.Find(id);
        }

        public IEnumerable<Employee> GetListOfEmployees()
        {
            return _dbContext.Employees;
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            var result = _dbContext.Employees.Attach(updatedEmployee);
            result.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return updatedEmployee;
        }
    }
}
