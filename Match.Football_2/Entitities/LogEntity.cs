using Microsoft.Azure.CosmosDB.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Entitities
{
    public class LogEntity : TableEntity
    {
        public LogEntity(string idLog, string idRow)
        {
            this.PartitionKey = idLog;
            this.RowKey = idRow;
            this.Timestamp = DateTime.UtcNow;
        }

        public LogEntity() { }

        public string MessageLog { get; set; }

        public string Date { get; set; }
    }
}
