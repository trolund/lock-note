using LockNote.Bl;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LockNote.FuncApp;

public class CleanData(ILogger<CleanData> logger, NotesService notesService)
{
    [Function("CleanData")]
    public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        _ = notesService.DeleteAllOverMonthOld();
        logger.LogWarning("Deleted all old notes");
    }
}