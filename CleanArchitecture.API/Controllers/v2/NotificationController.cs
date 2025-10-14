using CleanArchitecture.Service.IMangers.IFirebaseManager;
using CleanArchitecture.Service.Responseobject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers.v2
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    [Route("api/v{version:apiversion}[Controller]/[action]")]
    public class NotificationController(IFirebaseAuthenticate firebaseAuthenticate) : ControllerBase
    {
        private readonly IFirebaseAuthenticate _firebase = firebaseAuthenticate;
        [AllowAnonymous]
        [HttpGet]
       public async Task<ActionResult<ResponseResult<string>>> GetOAuthToken()
        {
 
            return Ok(await _firebase.GetAccessOAuthToken());
        }
    }
}
