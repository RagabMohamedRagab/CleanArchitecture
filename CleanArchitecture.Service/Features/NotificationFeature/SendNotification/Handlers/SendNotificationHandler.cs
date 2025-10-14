using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CleanArchitecture.Data.Enums;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Resources;
using CleanArchitecture.Service.Dtos.FireBaseDtos;
using CleanArchitecture.Service.Features.NotificationFeature.SendNotification.Commands;
using CleanArchitecture.Service.IMangers.IFirebaseManager;
using CleanArchitecture.Service.Responseobject;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Service.Features.NotificationFeature.SendNotification.Handlers
{
    public class SendNotificationHandler(IHttpClientFactory httpClientFactory,IOptions<FirebasePushNotifcation> options,ILogger<SendNotificationHandler> logger,IFirebaseAuthenticate firebaseAuthenticate) : IRequestHandler<SendNotificationCommand, ResponseResult<bool>>
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ClientFactoryKey.FireBaseSend.ToString());
        private readonly FirebasePushNotifcation _settingFirebase = options.Value;
        private readonly ILogger<SendNotificationHandler> _logger=logger;
        private readonly IFirebaseAuthenticate _firebaseAuthenticate=firebaseAuthenticate;
        public async Task<ResponseResult<bool>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            try {
                var Token = await _firebaseAuthenticate.GetAccessOAuthToken();
                _logger.LogInformation($"Step 1 : Get Access Token {Token}");

                var payload = new
                {
                    message = new
                    {
                        token = request.DeviceToken,
                        notification = new
                        {
                            title = request.Title,
                            body = request.Body
                        },
                        data = new
                        {
                            timestamp = DateTime.UtcNow.ToString("o"),
                            type = "notification"
                        },
                        android = new
                        {
                            priority = "high",
                            notification = new
                            {
                                sound = "default",
                                color = "#FF0000"
                            }
                        },
                        apns = new
                        {
                            headers = new
                            {
                                apns_priority = "10"
                            },
                            payload = new
                            {
                                aps = new
                                {
                                    sound = "default",
                                    badge = 1
                                }
                            }
                        }
                    }
                };

                var jsonBody = JsonSerializer.Serialize(payload, new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = false
                });
                var contentBody = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                _logger.LogInformation($"Step 2 : Build Body Of Notification {payload}");

                var BaseUrl = _httpClient.BaseAddress;

                _logger.LogInformation($"Step 3 : Firebase URl {BaseUrl}");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.Entity);

                var response = await _httpClient.PostAsync($"{_settingFirebase.ProjectId}/messages:send", contentBody);

                _logger.LogInformation($"Step 4 : Response of FireBase {response.StatusCode}");

                if (response.IsSuccessStatusCode) {
                    var result = response.Content.ReadAsStringAsync();
                    return new ResponseResult<bool>() { IsSuccessed = true, Status = response.StatusCode };
                }
                _logger.LogInformation($"Step 5 : Response Failed Firebase {response.StatusCode}");
                throw new BusineesException(MessageService.Failed_Request);
            }catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
