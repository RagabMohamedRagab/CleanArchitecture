

using System.Text;
using System.Text.Json;
using CleanArchitecture.Data.Enums;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Resources;
using CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay.Order;
using CleanArchitecture.Service.Features.PayPalFeatures.CreateOrder.Commands;
using CleanArchitecture.Service.IMangers.IPayPalManager;
using CleanArchitecture.Service.Responseobject;
using MediatR;

namespace CleanArchitecture.Service.Features.PayPalFeatures.CreateOrder.Handlers
{
    public class CreateOrderHandler(IPayPalAuthentication payPalAuthentication,IHttpClientFactory httpClientFactory) : IRequestHandler<CreateOrderCommand, ResponseResult<PayPalOrderResponse>>
    {
        private readonly HttpClient _httpClient=httpClientFactory.CreateClient(ClientFactoryKey.PayPal.ToString());
        private readonly IPayPalAuthentication _payPalAuthentication=payPalAuthentication;
      
        public async Task<ResponseResult<PayPalOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var GetTokenBearar =await _payPalAuthentication.AuthenticatePayPal();
            if (GetTokenBearar is null || String.IsNullOrEmpty(GetTokenBearar.access_token))
                throw new BusineesException(MessageService.Token_Is_Required_From_PayPal);
            // Bearer 

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(GetTokenBearar.token_type, GetTokenBearar.access_token);

            var jsonBody = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonBody, Encoding.UTF8,"application/json");

            var response=await _httpClient.PostAsync("v2/checkout/orders",content,cancellationToken);
            if (!response.IsSuccessStatusCode) {
                throw new BusineesException(MessageService.Failed_Request);
            }
            var result =await response.Content.ReadAsStringAsync();
            var resultApi = JsonSerializer.Deserialize<PayPalOrderResponse>(result);
            return new ResponseResult<PayPalOrderResponse>() { Entity= resultApi!, IsSuccessed=true,Status=System.Net.HttpStatusCode.OK};
        }
    }
}
