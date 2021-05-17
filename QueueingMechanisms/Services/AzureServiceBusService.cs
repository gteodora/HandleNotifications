using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;

namespace QueueingMechanisms.Services
{
    public class AzureServiceBusService : IQueueService
    {
        private readonly ILogger<AzureServiceBusService> logger;
        public AzureServiceBusService(ILogger<AzureServiceBusService> logger)
        {
            this.logger = logger;
        }
        public async Task InsertMessageAsync(string connectionString, string queueName, string message)
        {
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                ServiceBusSender sender = client.CreateSender(queueName);
                ServiceBusMessage messageServiceBus = new ServiceBusMessage(message);
                await sender.SendMessageAsync(messageServiceBus);
                logger.LogInformation($"Inserted: {message} to the queue: {queueName}");
            }
        }

         async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");
            await args.CompleteMessageAsync(args.Message);
        }

         Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            
            return Task.CompletedTask;
        }

        public async Task RetrieveMessageAsync(string connectionString, string  queueName)
        {
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
        }
    }
}
