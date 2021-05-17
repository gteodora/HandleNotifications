using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueingMechanisms.Services
{
    public interface IQueueService
    {
        public Task InsertMessageAsync(string connectionString, string queueName, string message);
         public Task RetrieveMessageAsync(string connectionString, string queueName);
    }
}
