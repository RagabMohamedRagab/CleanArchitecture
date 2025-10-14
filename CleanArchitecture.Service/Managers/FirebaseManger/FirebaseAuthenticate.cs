using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Data.Enums;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Resources;
using CleanArchitecture.Service.Dtos.FireBaseDtos;
using CleanArchitecture.Service.IMangers.IFirebaseManager;
using CleanArchitecture.Service.Responseobject;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Service.Managers.FirebaseManger
{
    public class FirebaseAuthenticate(IWebHostEnvironment hostEnvironment,IOptions<FirebasePushNotifcation> options,ILogger<FirebaseAuthenticate> logger,IHttpClientFactory httpClientFactory) : IFirebaseAuthenticate
    {
        private readonly IWebHostEnvironment _hostEnvironment =hostEnvironment;
        private readonly FirebasePushNotifcation _SettingsFirebase = options.Value;
        private readonly ILogger<FirebaseAuthenticate> _logger=logger;
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ClientFactoryKey.FireBase.ToString());
        public async Task<ResponseResult<string>> GetAccessOAuthToken()
        {
            try {
                _logger.LogInformation($"--- Step 1 : Enter Function GetToken with {_httpClient.BaseAddress} and Build Direct");

                var wwwroot = $"{_hostEnvironment.WebRootPath}\\FirebaseJson";
                var fullFireBase = Path.Combine(wwwroot, "firebase-adminsdk.json");

                _logger.LogInformation($"--STEP 2: Creating Google Credential with cloud-platform scope {_SettingsFirebase.GlobalScope}--");

                GoogleCredential credential;
                using (var stream = new FileStream(fullFireBase, FileMode.Open, FileAccess.Read)) {
                    credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(_SettingsFirebase.GlobalScope);
                }
                
                _logger.LogInformation($"--STEP 3: Requesting Access Token from Google OAuth--");

                var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

                return new ResponseResult<string>() { Entity = token, IsSuccessed = true, Message = MessageService.Sucess, Status = System.Net.HttpStatusCode.OK };
            }catch(BusineesException ex) {
                throw new BusineesException(MessageService.Token_Failed_To_Get);
            }
        }
    }
}
