using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.Models
{
    public interface IEmployeeRepo
    {
        Employee Add(Employee employee);
        Employee GetEmployee(int id);
        Employee UpdateEmployee(Employee updatedEmployee);
        IEnumerable<Employee> GetListOfEmployees();
        Employee DeleteEmployee(int id);


    }
}
