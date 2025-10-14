

using CleanArchitecture.Service.Responseobject;

namespace CleanArchitecture.Service.IMangers.IFirebaseManager
{
    public interface IFirebaseAuthenticate
    {

        public Task<ResponseResult<string>> GetAccessOAuthToken();
    }
}
