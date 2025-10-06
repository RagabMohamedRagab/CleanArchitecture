using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Service.Dtos.GateWay.Stripe;
using CleanArchitecture.Service.Responseobject;
using MediatR;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Stripe.Forwarding;

namespace CleanArchitecture.Service.Features.StripePaymentFeature.Commands
{
    public class StripePaymentCommand : IRequest<ResponseResult<Session>>
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public long Amount { get; set; }
        public string? Currency { get; set; }


        public int Quantity { get; set; }


        public class Handler : IRequestHandler<StripePaymentCommand, ResponseResult<Session>>
        {
            private readonly StripePayment _stripePayment;
            public Handler(IOptions<StripePayment> stripePayment)
            {
                _stripePayment = stripePayment.Value;
            }
            public async Task<ResponseResult<Session>> Handle(StripePaymentCommand request, CancellationToken cancellationToken)
            {
                try {
                    StripeConfiguration.ApiKey = _stripePayment.SecretKey;
                    var options = new SessionCreateOptions {
                        PaymentMethodTypes = new List<string> { "card" },
                        LineItems = new List<SessionLineItemOptions>
               {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = request.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = request.ProductName,
                            Description = request.ProductDescription,
                        },
                        UnitAmount = request.Amount,
                    },
                    Quantity = request.Quantity,
                },
            },
                        Mode = "payment",
                        SuccessUrl = _stripePayment.SuccessUrl,
                        CancelUrl = _stripePayment.SuccessUrl,
                    };
                    var service = new SessionService();
                    var session = service.Create(options);



                    return new ResponseResult<Session> { IsSuccessed = true, Status = System.Net.HttpStatusCode.OK, Entity = session };
                }

                catch (PaymentException ex) {

                    throw ex;
                }
            }
        }
    }
}
