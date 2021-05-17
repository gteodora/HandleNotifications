using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationsHandling.Services
{
    public interface INotificationHandlerService
    {
        public void HandleNotifications();
        public void InsertMessage(string type, string message);
        public void RetrieveMessage(string type);
    }
}
