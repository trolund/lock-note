using LockNote.End2EndTests.PageFacades;

namespace LockNote.End2EndTests.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MainTests : PageTest
{
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        var page = new FrontPage(Page);

        await page.GoToPageAsync();
        
        var title = await Page.TitleAsync();
        
        Assert.That(title, Is.EqualTo("LockNote - Share secrets safely"));

        await Page.PauseAsync();
        
        await page.EnterMessageAsync("This is a test message");
        await page.ClickSubmitAsync();
        
        var noteLink = await page.GetNoteLinkAsync();
        await page.ClickNoteLinkAsync();
        
        


    }
}