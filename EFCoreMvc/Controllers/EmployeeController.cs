using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreMvc.Controllers
{
    //[Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _repository;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            _repository = employeeRepo;
        }

        //[Route("")]
        //[Route("~/Home")]
        //[Route("~/")]
        public ViewResult Index()
        {
            var model = _repository.GetListOfEmployees();

            return View(model);
        }
        //[Route("{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _repository.GetEmployee(id);
            if (employee != null)
            {
                return View(employee);
            }
            Response.StatusCode = 404;
            return View("EmployeeNotFound", id);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            Employee updatedEmployee = _repository.UpdateEmployee(employee);
            if (updatedEmployee != null)
            {
                return RedirectToAction("Index", "Employee");
            }
            return View(updatedEmployee);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _repository.Add(employee);
                return RedirectToAction("Details", new { id = newEmployee.EmployeeId });
            }
            return View();
        }
        #region
        //After throwing an Exception
        //for testing purpose.
        public IActionResult Details(int id)
        {
            throw new Exception("tuseTheProgrammer just thrown an exception");
            Employee employee = _repository.GetEmployee(id);
            if (employee != null)
            {
                return View(employee);
            }

            Response.StatusCode = 404;
            return View("EmployeeNotFound", id);

        }
        #endregion
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee employee = _repository.GetEmployee(id);
            if (employee != null)
            {
                return View(employee);
            }
            Response.StatusCode = 404;
            return View("EmployeeNotFound", id);
        }
        [HttpPost]
        public IActionResult Delete(Employee model)
        {
            Employee employee = _repository.DeleteEmployee(model.EmployeeId);
            if (employee != null)
            {
                return RedirectToAction("Index", "Employee");
            }
            Response.StatusCode = 404;
            return View("EmployeeNotFound", model.EmployeeId);
        }
    }
}
