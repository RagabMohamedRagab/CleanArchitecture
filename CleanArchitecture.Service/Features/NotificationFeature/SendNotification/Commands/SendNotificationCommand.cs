using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Service.Responseobject;
using MediatR;

namespace CleanArchitecture.Service.Features.NotificationFeature.SendNotification.Commands
{
    public class SendNotificationCommand:IRequest<ResponseResult<bool>>
    {
        public string DeviceToken {  get; set; }
        public string Title { get; set; } 
        public string Body { get; set; } 
    }
}
