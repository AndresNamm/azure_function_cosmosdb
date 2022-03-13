using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace LocalFunctionProj
{
    public class MultiResponse
    {
        [CosmosDBOutput("my-database", "my-container",
            ConnectionStringSetting = "CosmosDbConnectionString", CreateIfNotExists = true)]
        public MyDocument Document { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }
    public class MyDocument
    {
        public string id { get; set; }
        public string message { get; set; }
    }

    public class HttpExample
    {
        private readonly ILogger _logger;

        public HttpExample(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpExample>();
        }

        [Function("HttpExample")]
        public MultiResponse Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
        {
            //_logger.LogInformation("C# HTTP trigger function processed a request.");
            var logger = executionContext.GetLogger("HttpExample");
            
            logger.LogInformation("C# Trigger processed request");
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            var message = "Welcome to Azure Functions!";
            response.WriteString(message);

            return new MultiResponse(){
                Document = new MyDocument{
                    id = System.Guid.NewGuid().ToString(),
                    message = message
                },
                HttpResponse = response
            };
        }
    }
}
