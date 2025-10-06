using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Data.Exceptions
{
    public class PaymentException : Exception
    {
        public PaymentException(string Message) : base(Message)
        {

        }


        public PaymentException(string Message, Exception ex) : base(Message, ex) { }
    }
}
