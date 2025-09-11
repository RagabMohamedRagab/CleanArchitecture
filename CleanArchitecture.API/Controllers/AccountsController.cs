using CleanArchitecture.Service.Dtos.AuthDtos;
using CleanArchitecture.Service.DTOS.AuthDTOS;
using CleanArchitecture.Service.IMangers.IAuthManger;
using CleanArchitecture.Service.Responseobject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountsController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService=authService;
        [HttpPost]
        public async Task<ResponseResult<ResponseAuthenticatedDto>> Regsiter([FromForm] RegsiterDto regsiter)
        {
          return  await _authService.Regsiter(regsiter);
        }

        [HttpPost]
        public async Task<ResponseResult<ResponseAuthenticatedDto>> Authenticate(LoginDto loginDto)
        {
            return await _authService.Authenticate(loginDto);
        }
    }
}
