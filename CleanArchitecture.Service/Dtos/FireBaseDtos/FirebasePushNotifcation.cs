using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.Dtos.FireBaseDtos
{
    public class FirebasePushNotifcation
    {
        public string? FireBaseAccount { get; set; }

        public string? Token {  get; set; }
        public string? Apikey { get; set; }
        public string? ProjectId { get; set; }
        public string? ProjectNumber { get; set; }
        public string? GlobalScope { get; set; }
        public string? AuthScope { get; set; }
    }
}
