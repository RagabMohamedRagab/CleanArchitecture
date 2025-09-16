using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class HomeController(ILogger<HomeController> logger) : ControllerBase
    {
        private readonly ILogger<HomeController> _logger=logger;
        [HttpGet]
        public string Test()
        {
            _logger.LogInformation("This is Information log");
            _logger.LogError("This is Error log");
            _logger.LogDebug("This is Debug log");
            _logger.LogWarning("this is WARING");
            return "welcome Application";
        }
    }
}
