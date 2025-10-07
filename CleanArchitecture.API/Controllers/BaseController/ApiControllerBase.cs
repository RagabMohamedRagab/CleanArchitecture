using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers.BaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator=mediator;


        //protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        //{
        //    return await _mediator.Send(query);
        //}

        protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
        {
            return await _mediator.Send(command);
        }
      


    }
}
