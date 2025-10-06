using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Data.Exceptions
{
    public class InvalidParametersException:Exception
    {
        public InvalidParametersException(string Message):base(Message)
        {
            
        }

        public InvalidParametersException(string Message,Exception ex):base(Message,ex)
        {
            
        }
    }
}
