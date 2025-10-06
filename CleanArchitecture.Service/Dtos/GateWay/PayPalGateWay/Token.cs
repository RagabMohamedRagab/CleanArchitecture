
namespace CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string nonce { get; set; }
    }
}
