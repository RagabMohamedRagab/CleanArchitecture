using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay
{
    public class PaypalPayment
    {
        public string ClientId { get; set; }
        public string ClientScret { get; set; }
        public string Url { get; set; }
        public string Auth { get; set; }
    }
}
