using System.Net;

namespace CleanArchitecture.Service.Responseobject
{
    public interface IResponse
    {

        public bool IsSuccessed { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Message {  get; set; }

        public string Error {  get; set; }

    }
}
