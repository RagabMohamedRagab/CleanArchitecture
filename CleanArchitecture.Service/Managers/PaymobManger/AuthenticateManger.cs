

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CleanArchitecture.Data.Enums;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Resources;
using CleanArchitecture.Service.Dtos.GateWay.PaymobGateWay;
using CleanArchitecture.Service.IMangers.IPaymobManger;
using CleanArchitecture.Service.Responseobject;
using Microsoft.Extensions.Options;
namespace CleanArchitecture.Service.Managers.PaymobManger
{
    public class AuthenticateManger(IHttpClientFactory httpClientFactory,IOptions<Paymob> options) : IAuthenticateManger
    {
        private readonly Paymob paymob=options.Value;
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ClientFactoryKey.Paymob.ToString());
        public async Task<ResponseResult<string>> GetAccessTokenAsync()
        {
            var body = new { api_key = paymob.APIKey };
            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response =await _httpClient.PostAsync("api/auth/tokens", content);

            if (response.IsSuccessStatusCode) {
                var result=await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(result);

                var token = doc.RootElement.GetProperty("token").GetString();
                return new ResponseResult<string>() {  Entity=token,IsSuccessed=true,Message=MessageService.Sucess};
            }
            throw new BusineesException(MessageService.Token_Failed_To_Get.ToString());

        }
    }
}
