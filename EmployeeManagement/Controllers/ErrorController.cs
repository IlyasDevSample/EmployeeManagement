using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> _logger)
        {
            logger = _logger;
        }

        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, The resource that you requested could not be found";
                    // LogWarning() method logs the message under
                    // Warning category in the log
                    logger.LogWarning($"404 error occured. Path = " +
                        $"{statusCodeResult.OriginalPath} and QueryString = " +
                        $"{statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFoundPage");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error() 
        {
            //var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //ViewBag.ExceptionPath = exceptionDetails.Path;
            //ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            //ViewBag.StackTrace = exceptionDetails.Error.StackTrace;

            // Retrieve the exception Details
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            // LogError() method logs the exception under Error category in the log
            logger.LogError($"The path {exceptionDetails.Path} " +
                $"threw an exception {exceptionDetails.Error}");

            return View("Error");
            
            
        }

    }
}
