using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay.Order
{
    public class PayPalOrderResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("payment_source")]
        public PayPalPaymentSource PaymentSource { get; set; }

        [JsonPropertyName("links")]
        public List<PayPalLink> Links { get; set; }
    }

    public class PayPalPaymentSource
    {
        [JsonPropertyName("paypal")]
        public PaypalResponse Paypal { get; set; }
    }

    public class PaypalResponse
    {
        // PayPal object is empty in your JSON, but you can extend if needed
    }

    public class PayPalLink
    {
        [JsonPropertyName("href")]

        public string Href { get; set; }
        [JsonPropertyName("rel")]

        public string Rel { get; set; }
        [JsonPropertyName("method")]

        public string Method { get; set; }
    }
}
