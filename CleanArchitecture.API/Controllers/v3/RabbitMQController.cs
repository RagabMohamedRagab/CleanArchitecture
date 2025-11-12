using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers.v3
{
    [Route("api/[controller]/[action]")]
    [Route("api/v{version:apiversion}[controller]/[action]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {

    }
}
