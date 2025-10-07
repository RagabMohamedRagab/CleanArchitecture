using CleanArchitecture.Service.IMangers.IPaymobManger;
using CleanArchitecture.Service.Responseobject;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers.v2
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    [Route("api/v{Version:apiVersion}/[Controller]/[action]")]
    public class PaymobController(IAuthenticateManger authenticateManger) : ControllerBase
    {
        private readonly IAuthenticateManger _Authentication = authenticateManger;
        [HttpGet]
        public async  Task<ActionResult<ResponseResult<string>>> GetAccessToken()
        {
            return await _Authentication.GetAccessTokenAsync();
        }
    }
}
