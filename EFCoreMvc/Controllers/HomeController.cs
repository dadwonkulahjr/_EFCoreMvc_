using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreMvc.Controllers
{
    //[Route("[controller]")]
    [Authorize]
    public class HomeController : Controller
    {

        //[Route("[action]")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["Message"] = "Hi, Welcome Dev tuseTheProgrammer";
            ViewData["TimeCheck"] = "The current time check in the city is " + DateTime.Now.ToLongTimeString();
            return View();
        }
       
    }
}
