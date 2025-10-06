using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CleanArchitecture.Data.Enums;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay;
using CleanArchitecture.Service.IMangers.IPayPalManager;
using Microsoft.Extensions.Options;
using CleanArchitecture.Resources;
namespace CleanArchitecture.Service.Managers.PayPalManager
{
    public class PayPalAuthentication(IOptions<PaypalPayment> options, IHttpClientFactory httpClientFactory) :  IPayPalAuthentication
    {
        private readonly PaypalPayment _payment = options.Value;
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ClientFactoryKey.PayPal.ToString());
        public async Task<Token> AuthenticatePayPal()
        {
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_payment.ClientId}:{_payment.ClientScret}"));
            _httpClient.DefaultRequestHeaders.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
            // Like Body grant_type :client_credentials
            var x_www_Form_Urlencoded = new Dictionary<string, string> 
            {
                {"grant_type","client_credentials"} 
            };

            var content = new FormUrlEncodedContent(x_www_Form_Urlencoded);

            HttpResponseMessage response = await _httpClient.PostAsync("v1/oauth2/token", content);
            if (response.IsSuccessStatusCode) {
                var jsonBody=await response.Content.ReadAsStringAsync();
                return  JsonSerializer.Deserialize<Token>(jsonBody);
            }

            throw new BusineesException(MessageService.Token_Failed_To_Get);
        }
    }



}
