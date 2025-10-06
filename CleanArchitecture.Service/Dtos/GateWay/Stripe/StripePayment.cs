using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.Dtos.GateWay.Stripe
{
    public class StripePayment
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
