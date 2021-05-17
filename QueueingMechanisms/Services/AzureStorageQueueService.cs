using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // Namespace for ConfigurationManager
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage
using Microsoft.Extensions.Logging;

namespace QueueingMechanisms.Services
{
    public class AzureStorageQueueService : IQueueService
    {
        private readonly ILogger<AzureStorageQueueService> logger;
        public AzureStorageQueueService(ILogger<AzureStorageQueueService> logger)
        {
            this.logger = logger;
        }

        public async Task InsertMessageAsync(string connectionString, string queueName, string message)
        {
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            await queueClient.CreateIfNotExistsAsync();

            if (await queueClient.ExistsAsync())
            {
                logger.LogInformation($"Queue '{queueClient.Name}' created");
            }
            else
            {
                logger.LogInformation($"Queue '{queueClient.Name}' exists");
            }
            await queueClient.SendMessageAsync(message);
            logger.LogInformation($"Inserted: {message}");
        }

        public async Task RetrieveMessageAsync(string connectionString, string queueName)
        {
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            await queueClient.CreateIfNotExistsAsync();
            if (await queueClient.ExistsAsync())
            {
                logger.LogInformation($"Queue '{queueClient.Name}' created");
            }
            else
            {
                logger.LogInformation($"Queue '{queueClient.Name}' exists");
            }
            QueueMessage[] retrievedMessage = await queueClient.ReceiveMessagesAsync();
            logger.LogInformation($"Retrieved message with id '{retrievedMessage[0].MessageId}'");
        }
    }
}
