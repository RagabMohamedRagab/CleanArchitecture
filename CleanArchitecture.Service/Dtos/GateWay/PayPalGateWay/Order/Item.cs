using System.Text.Json.Serialization;



public class PaymentSource
{
    [JsonPropertyName("paypal")]
    public Paypal Paypal { get; set; }
}

public class Paypal
{
    [JsonPropertyName("experience_context")]
    public ExperienceContext ExperienceContext { get; set; }
}

public class ExperienceContext
{
    [JsonPropertyName("payment_method_preference")]
    public string PaymentMethodPreference { get; set; }

    [JsonPropertyName("landing_page")]
    public string LandingPage { get; set; }

    [JsonPropertyName("shipping_preference")]
    public string ShippingPreference { get; set; }

    [JsonPropertyName("user_action")]
    public string UserAction { get; set; }

    [JsonPropertyName("return_url")]
    public string ReturnUrl { get; set; }

    [JsonPropertyName("cancel_url")]
    public string CancelUrl { get; set; }
}

public class PurchaseUnit
{
    [JsonPropertyName("invoice_id")]
    public string InvoiceId { get; set; }

    [JsonPropertyName("amount")]
    public Amount Amount { get; set; }

    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }
}

public class Amount
{
    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("breakdown")]
    public Breakdown Breakdown { get; set; }
}

public class Breakdown
{
    [JsonPropertyName("item_total")]
    public Money ItemTotal { get; set; }

    [JsonPropertyName("shipping")]
    public Money Shipping { get; set; }
}

public class Money
{
    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class Item
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("unit_amount")]
    public Money UnitAmount { get; set; }

    [JsonPropertyName("quantity")]
    public string Quantity { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("sku")]
    public string Sku { get; set; }

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("upc")]
    public Upc Upc { get; set; }
}

public class Upc
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }
}
