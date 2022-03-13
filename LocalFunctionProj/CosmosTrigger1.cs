using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class CosmosTrigger1
    {
        private readonly ILogger _logger;

        public CosmosTrigger1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CosmosTrigger1>();
        }

        [Function("CosmosTrigger1")]
        public void Run([CosmosDBTrigger(
            databaseName: "my-database",
            collectionName: "my-container",
            ConnectionStringSetting = "andresdevcosmosdb_DOCUMENTDB",
            LeaseCollectionName = "leases",CreateLeaseCollectionIfNotExists =true)] IReadOnlyList<MyDocument> input)
        {
            if (input != null && input.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + input.Count);
                _logger.LogInformation("First document Id: " + input[0].Id);
            }
        }
    }

    public class MyDocument
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public bool Boolean { get; set; }
    }
}
