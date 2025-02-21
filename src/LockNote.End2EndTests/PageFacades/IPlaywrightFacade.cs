namespace LockNote.End2EndTests.PageFacades;

public interface IPlaywrightFacade
{
    protected Task<PlaywrightFacade> GoToPageAsync();
}