using CleanArchitecture.Service.Features.PaymobFeature.CreateOrder.Commands;
using CleanArchitecture.Service.IMangers.IPaymobManger;
using CleanArchitecture.Service.Responseobject;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers.v2
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    [Route("api/v{Version:apiVersion}/[Controller]/[action]")]
    public class PaymobController(IAuthenticateManger authenticateManger, IMediator mediator) : ControllerBase
    {
        private readonly IAuthenticateManger _Authentication = authenticateManger;
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        public async  Task<ActionResult<ResponseResult<string>>> GetAccessToken()
        {
            return await _Authentication.GetAccessTokenAsync();
        }
        [HttpPost]
        public async Task<ActionResult<ResponseResult<PaymobIntentionResponse>>> CreateOrder([FromBody]CreatePaymobOrderCommand  createOrderCommand)
        {
            return await _mediator.Send(createOrderCommand);
        }
    }
}
