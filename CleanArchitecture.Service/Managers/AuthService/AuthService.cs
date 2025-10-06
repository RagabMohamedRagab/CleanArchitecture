

using System.Threading.Tasks;
using CleanArchitecture.Data.Entities.Identities;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Data.IEmailService;
using CleanArchitecture.Data.IRpositories.IAuth;
using CleanArchitecture.Service.Dtos.AuthDtos;
using CleanArchitecture.Service.DTOS.AuthDTOS;
using CleanArchitecture.Service.Helpers;
using CleanArchitecture.Service.IMangers.IAuthManger;
using CleanArchitecture.Service.Responseobject;

namespace CleanArchitecture.Service.Managers.AuthService
{
    public class AuthService(IAuthRepository authRepository,AuthJwt authJwt,IEmailSender emailSender) : IAuthService
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly AuthJwt _authJwt=authJwt;
        private readonly IEmailSender _emailSender = emailSender;
        public async Task<ResponseResult<ResponseAuthenticatedDto>> Authenticate(LoginDto loginDto)
        {
            if (loginDto is null) {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var isSave= await _authRepository.Authenticate(loginDto.Email, loginDto.Password);
            if (!isSave)
                throw new FaliedRequestException($"Failed To Login User with Email:  {loginDto.Email} and Password :{loginDto.Password} ");
            await _emailSender.SendEmailAsync(loginDto.Email, "Application Confirm","Thanks For Login");
            return new ResponseResult<ResponseAuthenticatedDto> {
                Entity = new ResponseAuthenticatedDto {
                                    Email = loginDto.Email,
                                    Password = loginDto.Password, 
                                    Token= _authJwt.GenerateToken(loginDto.Email)
                                    },
                                  IsSuccessed = true
             };
        }

        public async Task<ResponseResult<ResponseAuthenticatedDto>> Regsiter(RegsiterDto regsiter)
        {
            if (regsiter is null) { throw new ArgumentNullException(nameof(regsiter)); }
            var userapp = new UserApp() {
                Id=Guid.NewGuid().ToString(),
                Email = regsiter.Email,
                NormalizedEmail = regsiter.Email.ToUpper(),
                UserName = regsiter.Name,
                NormalizedUserName = regsiter.Name.ToUpper(),
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
            };

          var isaved= await _authRepository.Regsitertion(userapp, regsiter.Password);
            if (!isaved)
                throw new FaliedRequestException($"Failed To Login User with Email:  {regsiter.Email} and Password :{regsiter.Password} ");
             await _emailSender.SendEmailAsync(regsiter.Email, "Regsitertion Complete", "Done");
            return new ResponseResult<ResponseAuthenticatedDto> {
                Entity = new ResponseAuthenticatedDto {
                    Email = regsiter.Email,
                    Password = regsiter.Password,
                    Token = _authJwt.GenerateToken(regsiter.Email)
                },
                IsSuccessed = true
            };
        }
    }
}
