using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay;

namespace CleanArchitecture.Service.IMangers.IPayPalManager
{
    public interface IPayPalAuthentication
    {
        Task<Token> AuthenticatePayPal();
    }
}
