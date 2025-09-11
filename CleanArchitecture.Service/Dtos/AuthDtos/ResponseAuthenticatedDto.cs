using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.Dtos.AuthDtos
{
    public class ResponseAuthenticatedDto
    {
        public string Email{ get; set; }
        public string Password{ get; set; }
        public string Token{ get; set; }
    }
}
