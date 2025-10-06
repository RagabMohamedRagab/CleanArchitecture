

namespace CleanArchitecture.Data.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string Message):base(Message) { }
        public NotFoundException(string message,Exception ex):base(message,ex){}
    }
}
