using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Service.Dtos.RedisDtos;
using CleanArchitecture.Service.Responseobject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CleanArchitecture.API.Controllers.v3
{
    [Route("api/[controller]/[action]")]
    [Route("api/v{version:apiversion:}[controller]/[action]")]
    [ApiController]
    public class RedisController(IDistributedCache distributedCache) : ControllerBase
    {
        private readonly IDistributedCache _distributedCache = distributedCache;


        [HttpPost("{key}")]
        public async Task<ResponseResult<bool>> AddCacheAsync(string key, [FromBody] RedisObject redisObject)
        {
            try {
                var serleize = JsonConvert.SerializeObject(redisObject);

                _distributedCache.SetString(key, serleize);
                return new ResponseResult<bool>(true);
            }
            catch (FaliedRequestException ex) {
                return new ResponseResult<bool>(false);
            }
        }
        [HttpGet("{Key}")]
        public async Task<ResponseResult<RedisObject>> GetByKey(string Key)
        {
            RedisObject redisObject = null;
            var obj = _distributedCache.GetString(Key);
            if (obj != null) {
                redisObject = JsonConvert.DeserializeObject<RedisObject>(obj);

                return new ResponseResult<RedisObject>(redisObject);
            }
            return new ResponseResult<RedisObject>(redisObject);
        }

        [HttpDelete("{key}")]

        public async Task<ResponseResult<bool>> RemoveByKey(string Key)
        {
            try {
                _distributedCache.Remove(Key);
                return new ResponseResult<bool>(true);
            }
            catch (FaliedRequestException ex) {
                return new ResponseResult<bool>(false);
            }
        }
    }
}
