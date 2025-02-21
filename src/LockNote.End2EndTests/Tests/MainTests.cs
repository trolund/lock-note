using LockNote.End2EndTests.PageFacades;

namespace LockNote.End2EndTests.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MainTests : PageTest
{
    [TestCase("This is a test message")]
    [TestCase("This is a test message \ud83d\ude80")]
    [TestCase("123_THE_MESSAGE_123")]
    public async Task WHEN_ANoteIsCreated_THEN_TheNoteIsStored(string message)
    {
        var page = new FrontPage(Page);
        
        // Go to the page
        await page.GoToPageAsync();
        
        // Enter the message and submit
        await page.EnterMessageAsync(message);
        await page.ClickSubmitAsync();
        
        // get the note page to read the message
        await page.ClickNoteLinkAsync();
        
        var notePage = new ReadNotePage(Page);
        
        // read the message
        var storedMessage = await notePage.GetMessageAsync();
        
        Assert.That(message, Is.EqualTo(storedMessage));
    }
}