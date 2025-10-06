using System.Text.Json.Serialization;
using CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay.Order;
using CleanArchitecture.Service.Responseobject;
using MediatR;

namespace CleanArchitecture.Service.Features.PayPalFeatures.CreateOrder.Commands
{
    public class CreateOrderCommand:IRequest<ResponseResult<PayPalOrderResponse>>
    {
        [JsonPropertyName("intent")]

        public string Intent { get; set; } = "CAPTURE";

        [JsonPropertyName("purchase_units")]
        public List<PurchaseUnit> PurchaseUnits { get; set; }
        [JsonPropertyName("payment_source")]
        public PaymentSource PaymentSource { get; set; }

    }
}
