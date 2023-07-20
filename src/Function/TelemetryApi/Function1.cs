using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace TelemetryApi
{
    public static class Function1
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "TelemetryHub")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName(nameof(ReceiveTelemery))]
        public static void ReceiveTelemery([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req) 
        {
            try
            {

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}