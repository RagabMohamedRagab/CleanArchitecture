

namespace CleanArchitecture.Data.Exceptions
{
    public class FaliedRequestException:Exception
    {
        public FaliedRequestException(string Message):base(Message) {}

        public FaliedRequestException(string Message,Exception ex):base(Message,ex) 
        {
            
        }
    }
}
