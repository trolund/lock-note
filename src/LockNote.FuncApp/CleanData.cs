using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace LockNote.FuncApp;

public class CleanData(ILogger<CleanData> logger)
{
    [Function("CleanData")]
    public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        logger.LogInformation("C# Timer trigger function executed at: {DateTime.Now}");
        
        logger.LogWarning("This is a warning message");

        if (myTimer.ScheduleStatus is not null)
        {
            logger.LogInformation("Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            
        }
    }
    
    // http trigger
    [Function("CleanDataHttp")]
    public HttpResponseData RunHttp([HttpTrigger("get")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString("Hello, world!");
        return response;
    }
}