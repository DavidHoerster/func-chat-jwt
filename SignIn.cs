using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net;

namespace Agile.SigR
{
    public static class SignIn
    {
        [FunctionName("signin")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "options", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var signalR = new AzureSignalR(Environment.GetEnvironmentVariable("AzureSignalRConnectionString"));
            return req.CreateResponse(HttpStatusCode.OK, new
            {
                authInfo = new
                {
                    serviceUrl = signalR.GetClientHubUrl("chat"),
                    accessToken = signalR.GenerateAccessToken("chat")
                }
            }, "application/json");
        }
    }
}
