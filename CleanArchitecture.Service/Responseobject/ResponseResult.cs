

using System.Net;

namespace CleanArchitecture.Service.Responseobject
{
    public class ResponseResult<TEntity> : IResponseResult<TEntity>
    {
        public ResponseResult()
        {
            
        }
        // For Entity

        public ResponseResult(TEntity entity):this(entity,true,HttpStatusCode.OK,null,null)
        {
            
        }

        public ResponseResult(TEntity entity,bool isSuccessed) : this(entity, isSuccessed, HttpStatusCode.OK, null, null)
        {

        }

        public ResponseResult(TEntity entity, bool isSuccessed,HttpStatusCode status) : this(entity, isSuccessed, status, null, null)
        {

        }
        public ResponseResult(TEntity entity, bool isSuccessed, HttpStatusCode status,string message) : this(entity, isSuccessed, status, message, null)
        {

        }

        public ResponseResult(TEntity entity,bool isSuccessed,HttpStatusCode status,string message,string error)
        {
            Entity = entity;
            IsSuccessed = isSuccessed;
            Status = status;
            Message = message;
            Error = error;

        }
        public TEntity Entity { get; set; }
        public bool IsSuccessed { get ; set ; }
        public HttpStatusCode Status { get; set; }
        public string Message { get ; set ; }
        public string Error { get ; set ; }
    }
}
