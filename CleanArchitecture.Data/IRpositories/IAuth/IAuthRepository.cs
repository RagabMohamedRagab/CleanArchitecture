using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Data.Entities.Identities;

namespace CleanArchitecture.Data.IRpositories.IAuth
{
    public interface IAuthRepository
    {

        Task<bool> Regsitertion(UserApp userApp,string Password);

        Task<bool> Authenticate(string Email, string Password);

    }
}
