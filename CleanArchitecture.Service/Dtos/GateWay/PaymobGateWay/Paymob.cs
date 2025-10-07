

namespace CleanArchitecture.Service.Dtos.GateWay.PaymobGateWay
{
    public class Paymob
    {
        public string APIKey { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public string HMAC { get; set; }
        public string Auth { get; set; }
    }
}
