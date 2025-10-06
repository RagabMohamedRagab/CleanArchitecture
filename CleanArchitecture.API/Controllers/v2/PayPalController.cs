using CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay;
using CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay.Order;
using CleanArchitecture.Service.Features.PayPalFeatures.CreateOrder.Commands;
using CleanArchitecture.Service.IMangers.IPayPalManager;
using CleanArchitecture.Service.Responseobject;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers.v2
{
    [Route("api/[controller]/[action]")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class PayPalController(IPayPalAuthentication authentication,IMediator mediator) : ControllerBase
    {
        private readonly IPayPalAuthentication _authentication =authentication;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<Token>> GetToken()
        {
            return await _authentication.AuthenticatePayPal();
        }
        [HttpPost("CreatePayPalOrder")]
        public async Task<ActionResult<ResponseResult<PayPalOrderResponse>>> CreateOrder([FromBody] CreateOrderCommand createOrderCommand )
        {
          return(await _mediator.Send(createOrderCommand));
        }
    }
}
