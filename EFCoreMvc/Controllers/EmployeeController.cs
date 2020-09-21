using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _repository;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            _repository = employeeRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> model = _repository.GetListOfEmployees();

            return View(model);
        }
        public IActionResult Edit(int id)
        {
            Employee employee = _repository.GetEmployee(id);
            if(employee != null)
            {
                return View(employee);
            }

            return RedirectToAction("/Error");
        }
    }
}
