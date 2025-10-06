using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Data.Exceptions
{
    public class BusineesException:Exception
    {
        public BusineesException(string Message):base(Message) { }

        public BusineesException(string Message,Exception ex):base(Message,ex) { }
        

    }
}
