using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EFCoreMvc.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("/Error/{errorHandler}")]
        public IActionResult ErrorHandler(int errorhandler)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (errorhandler)
            {
                case 404:
                    ViewBag.ErrorMessage = "The resource you are looking for cannot be found!";
                    _logger.LogInformation($"The following path {statusCodeResult.OriginalPath} throw an exception" +
                        $"The query string value is {statusCodeResult.OriginalQueryString}");
                    break;

            }
            return View("ErrorHandler");
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var result = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewData["ExceptionMessage"] = result.Error.Message;
            _logger.LogError($"Error message: {result.Error.Message}" +
                $"ExceptionPath: {result.Path}" +
                $"ExceptionStackTrace: {result.Error.StackTrace}");

            return View("Error");
        }
    }
}
