using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("This is Information log");
            _logger.LogError("This is Error log");
            _logger.LogDebug("This is Debug log");
            _logger.LogWarning("this is WARING");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
