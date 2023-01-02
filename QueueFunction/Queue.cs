using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueFunction
{
    public class Order
    {
        public string OrderID { get; set; }
        public int Quantity { get; set; }
    }

    public class TableOrder
    {
        #region NEEDED FOR AZURE TABLE STORAGE
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        #endregion

    }

    public class Queue
    {
        //connectionString is defined in local.settings.json
        //return => WE USE THIS TO REFER THAT THE TRIGGER WILL WRITE TO TABLE STORAGE
        //CONNECTION IS THE SAME BECAUSE OF THE SAME STORAGE ACCOUNT
        [FunctionName("GetMessages")]
        [return: Table("Orders", Connection = "connectionString")]
        public TableOrder Run([QueueTrigger("appqueue", Connection = "connectionString")] Order order, ILogger log)
        {
            TableOrder tableOrder = new TableOrder()
            {
                PartitionKey = order.OrderID,
                RowKey = Convert.ToString(order.Quantity)
            };

            //EXPECTS THAT MESSAGES WERE SENT AS BASE64ENCODED
            //log.LogInformation($"ORDER ID: {order.OrderID}");
            //log.LogInformation($"QUANTITY: {order.Quantity}");

            log.LogInformation($"Order information has been written to AzureTableStorage");

            return tableOrder;
        }
    }
}
