
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CleanArchitecture.Data.Enums;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Resources;
using CleanArchitecture.Service.Features.PaymobFeature.CreateOrder.Commands;
using CleanArchitecture.Service.IMangers.IPaymobManger;
using CleanArchitecture.Service.IMangers.IPayPalManager;
using CleanArchitecture.Service.Responseobject;
using MediatR;

namespace CleanArchitecture.Service.Features.PaymobFeature.CreateOrder.Handlers
{
    public class CreateOrderHandler(IAuthenticateManger  authenticateManger, IHttpClientFactory httpClientFactory) : IRequestHandler<CreatePaymobOrderCommand, ResponseResult<PaymobIntentionResponse>>
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ClientFactoryKey.Paymob.ToString());
        private readonly IAuthenticateManger  _authenticate = authenticateManger;
        public async Task<ResponseResult<PaymobIntentionResponse>> Handle(CreatePaymobOrderCommand request, CancellationToken cancellationToken)
        {
            var token=await _authenticate.GetAccessTokenAsync();
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Token",token.Entity);

            var response = await _httpClient.PostAsync("v1/intention", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) {
                var paymobResponse = JsonSerializer.Deserialize<ResponseResult<PaymobIntentionResponse>>(responseContent);
                return paymobResponse;
            }
            throw new BusineesException(MessageService.Failed_Request);
        }
    }
}
