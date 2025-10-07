

using CleanArchitecture.Service.Responseobject;

namespace CleanArchitecture.Service.IMangers.IPaymobManger
{
    public interface IAuthenticateManger
    {
        public Task<ResponseResult<string>> GetAccessTokenAsync();
    }
}
