using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LockNote.FuncApp;

public class CleanData
{
    private readonly ILogger<CleanData> _logger;

    public CleanData(ILogger<CleanData> logger)
    {
        _logger = logger;
    }

    [Function("CleanData")]
    public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            
        }
    }
}