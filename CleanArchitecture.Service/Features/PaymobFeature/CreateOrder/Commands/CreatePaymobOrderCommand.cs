using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Service.Responseobject;
using MediatR;

namespace CleanArchitecture.Service.Features.PaymobFeature.CreateOrder.Commands
{
    public class CreatePaymobOrderCommand : IRequest<ResponseResult<PaymobIntentionResponse>>
    {

        public int amount { get; set; }
        public string currency { get; set; }
        public List<int> payment_methods { get; set; }
        public int? subscription_plan_id { get; set; }
        public string subscription_start_date { get; set; }

        public List<Product> items { get; set; }
        public BillingData billing_data { get; set; }
        public Customer customer { get; set; }
        public Dictionary<string, object> extras { get; set; }
    }
        public class Product
        {
            public string name { get; set; }
            public int amount { get; set; }
            public string description { get; set; }
            public int quantity { get; set; }
        }

        public class BillingData
        {
            public string apartment { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string street { get; set; }
            public string building { get; set; }
            public string phone_number { get; set; }
            public string country { get; set; }
            public string email { get; set; }
            public string floor { get; set; }
            public string state { get; set; }
        }

        public class Customer
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public Dictionary<string, object> extras { get; set; }
        }

    }
