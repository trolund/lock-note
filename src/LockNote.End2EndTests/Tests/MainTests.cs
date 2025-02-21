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

    [TestCase("This is a test message", "password")]
    [TestCase("This is a test message \ud83d\ude80", "password")]
    [TestCase("123_THE_MESSAGE_123", "password")]
    [TestCase("This is a test message", "123456")]
    [TestCase("This is a test message \ud83d\ude80", "123456")]
    [TestCase("123_THE_MESSAGE_123", "123456")]
    [TestCase("This is a test message", "password-123-123-123-hi-this-is-a-good-password")]
    [TestCase("This is a test message \ud83d\ude80", "password-123-123-123-hi-this-is-a-good-password")]
    [TestCase("123_THE_MESSAGE_123", "p")]
    public async Task WHEN_ANoteIsCreatedWithAPassword_THEN_TheNoteIsStored_AND_canOnlyBeReadWithTheCorrectPassword(string message, string password)
    {
        var page = new FrontPage(Page);
        
        // Go to the page
        await page.GoToPageAsync();
        
        // Enter the message and submit
        await page.EnterMessageAsync(message);
        // Toggle the advanced fields
        await page.ToggleAdvancedFieldsAsync();
        // Enter the password
        await page.EnterPasswordAsync(password);
        await page.EnterPasswordAgainAsync(password);
        
        await page.ClickSubmitAsync();
        
        // get the note page to read the message
        await page.ClickNoteLinkAsync();
        
        var notePage = new ReadNotePage(Page);
        
        await notePage.EnterPasswordAsync(password);
        await notePage.ClickSubmitAsync();
        
        // read the message
        var storedMessage = await notePage.GetMessageAsync();
        
        Assert.That(message, Is.EqualTo(storedMessage));
    }
}