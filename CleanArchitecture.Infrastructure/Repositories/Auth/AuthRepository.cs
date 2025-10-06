using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Data.Entities.Identities;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Data.IRpositories.IAuth;
using CleanArchitecture.Resources;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Repositories.Auth
{
    public class AuthRepository(UserManager<UserApp> userManager, SignInManager<UserApp> signInManager) : IAuthRepository
    {
        private readonly UserManager<UserApp> _userManager = userManager;
        private readonly SignInManager<UserApp> _signInManager = signInManager;
        public async Task<bool> Authenticate(string Email, string Password)
        {
            var user=await _userManager.FindByEmailAsync(Email);
            if (user == null)
                throw new NotFoundException(MessageService.User_Not_Found);
            var isSignIn=await _signInManager.PasswordSignInAsync(user, Password,isPersistent:false,false);
            return isSignIn.Succeeded;
        }

        public async Task<bool> Regsitertion(UserApp user, string Password)
        {
           
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded) {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }
            return false;
        }
    }
}
