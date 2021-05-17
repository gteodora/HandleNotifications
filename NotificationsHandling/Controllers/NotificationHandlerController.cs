using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationsHandling.Services;
using QueueingMechanisms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationsHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationHandlerController : ControllerBase
    {
        private readonly ILogger<NotificationHandlerController> logger;
        private readonly INotificationHandlerService service;

        public NotificationHandlerController(INotificationHandlerService service, ILogger<NotificationHandlerController> logger)
        {
            this.logger = logger;
            this.service = service;
            
        }
    }
}
