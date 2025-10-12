namespace CleanArchitecture.Service.Features.PaymobFeature.CreateOrder.Commands
{
    

    public class PaymobIntentionResponse
    {
        public List<PaymentKey> payment_keys { get; set; }
        public string id { get; set; }
        public IntentionDetail intention_detail { get; set; }
        public string client_secret { get; set; }
        public List<PaymentMethod> payment_methods { get; set; }
        public object special_reference { get; set; }
        public Extras extras { get; set; }
        public bool confirmed { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public object card_detail { get; set; }
        public List<object> card_tokens { get; set; }
        public string @object { get; set; }
    }

    public class PaymentKey
    {
        public int integration { get; set; }
        public string key { get; set; }
        public string gateway_type { get; set; }
        public object iframe_id { get; set; }
    }

    public class IntentionDetail
    {
        public int amount { get; set; }
        public List<product> items { get; set; }
        public string currency { get; set; }
        public BillData billing_data { get; set; }
    }

    public class product
    {
        public string name { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public object image { get; set; }
    }

    public class BillData
    {
        public string apartment { get; set; }
        public string floor { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string street { get; set; }
        public string building { get; set; }
        public string phone_number { get; set; }
        public string shipping_method { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string email { get; set; }
        public string postal_code { get; set; }
    }

    public class PaymentMethod
    {
        public int integration_id { get; set; }
        public string alias { get; set; }
        public string name { get; set; }
        public string method_type { get; set; }
        public string currency { get; set; }
        public bool live { get; set; }
        public bool use_cvc_with_moto { get; set; }
    }

    public class Extras
    {
        public CreationExtras creation_extras { get; set; }
        public object confirmation_extras { get; set; }
    }

    public class CreationExtras
    {
        public int ee { get; set; }
    }

}
