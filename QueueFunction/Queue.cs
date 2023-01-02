using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueFunction
{
    public class Queue
    {
        [FunctionName("GetMessages")]
        public void Run([QueueTrigger("appqueue", Connection = "connectionString")]string myQueueItem, ILogger log)
        {
            //EXPECTS THAT MESSAGES WERE SENT AS BASE64ENCODED
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
