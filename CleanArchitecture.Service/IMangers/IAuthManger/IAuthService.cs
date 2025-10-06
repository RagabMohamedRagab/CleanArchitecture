

using CleanArchitecture.Service.Dtos.AuthDtos;
using CleanArchitecture.Service.DTOS.AuthDTOS;
using CleanArchitecture.Service.Responseobject;

namespace CleanArchitecture.Service.IMangers.IAuthManger
{
    public interface IAuthService
    {
         Task<ResponseResult<ResponseAuthenticatedDto>> Authenticate(LoginDto loginDto);

         Task<ResponseResult<ResponseAuthenticatedDto>> Regsiter(RegsiterDto regsiter);
    }
}
