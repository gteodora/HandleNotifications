using Microsoft.Extensions.Logging;
using QueueingMechanisms.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationsHandling.Services
{
    public class NotificationHandlerService : INotificationHandlerService
    {
        private readonly ILogger<NotificationHandlerService> logger;
        private readonly IQueueService service;

        public NotificationHandlerService(IQueueService service, ILogger<NotificationHandlerService> logger)
        {
            this.logger = logger;
            this.service = service;
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["StorageConnectionString"];
        }

        public void InsertMessage(string type, string message)
        {
            string connectionString = GetConnectionString();

            service.InsertMessageAsync(connectionString, type, message);
        }
        
        public void RetrieveMessage(string type)
        {
            string connectionString = GetConnectionString();

            service.RetrieveMessageAsync(connectionString, type);
        }

        //request gives the information about the person who needs to receive the message
        //person can be subsribed to more chanells
        //it is needed to sent that message to every chanell that person is asigned
        public void HandleNotifications()
        {
            //creating request data
            var message = "Test message";
            string[] subsribedTo = { Person.SUBSCRIBED_TO_EMAIL, Person.SUBSCRIBED_TO_SMS, Person.SUBSCRIBED_TO_PUSH };
            Person personTestData = new Person(subsribedTo);
            //putting to queue
            foreach (string chanell in personTestData.SubsribedTo)
            {
                InsertMessage(chanell, message);
            }
        }
    }
}
