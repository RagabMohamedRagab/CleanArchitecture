using CleanArchitecture.Service.Features.NotificationFeature.SendNotification.Commands;
using CleanArchitecture.Service.IMangers.IFirebaseManager;
using CleanArchitecture.Service.Responseobject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers.v2
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    [Route("api/v{version:apiversion}[Controller]/[action]")]
    public class NotificationController(IMediator mediator,IFirebaseAuthenticate firebaseAuthenticate) : ControllerBase
    {
        private readonly IMediator _mediator=mediator;
        private readonly IFirebaseAuthenticate _firebase = firebaseAuthenticate;
        [AllowAnonymous]
        [HttpGet]
       public async Task<ActionResult<ResponseResult<string>>> GetOAuthToken()
        {
            return Ok(await _firebase.GetAccessOAuthToken());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseResult<bool>>> SendNotification([FromBody] SendNotificationCommand command)
        {
          return await _mediator.Send(command);
        }

    }
}
