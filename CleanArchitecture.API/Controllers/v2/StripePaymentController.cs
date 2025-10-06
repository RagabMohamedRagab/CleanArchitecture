using CleanArchitecture.API.Controllers.BaseController;
using CleanArchitecture.Service.Features.StripePaymentFeature.Commands;
using CleanArchitecture.Service.Responseobject;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace CleanArchitecture.API.Controllers.v2
{
    [Route("api/[controller]/[action]")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : ApiControllerBase
    {
        public StripePaymentController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<ResponseResult<Session>>> StripePayment(StripePaymentCommand command)
        {
            return await CommandAsync(command);
        }
    }
}
