namespace LockNote.End2EndTests.PageFacades;

using System.Threading.Tasks;
using Microsoft.Playwright;

public class FrontPage(IPage page) : PlaywrightFacade(page, "/")
{
    private readonly IPage _page = page;

    public async Task EnterMessageAsync(string message)
    {
        var selector = _page.GetByTestId("message");
        await selector.FillAsync(message);
    }

    public async Task<string> GetMessageErrorAsync()
    {
        var selector = _page.GetByTestId("message-error");
        return await selector.InnerTextAsync();
    }

    public async Task ToggleAdvancedFieldsAsync()
    {
        var selector = _page.GetByTestId("expand-button");
        await selector.ClickAsync();
    }

    public async Task SelectNumberOfViewsAsync(int numOfViews)
    {
        await _page.SelectOptionAsync("select[title='number of reads']", numOfViews.ToString());
    }

    public async Task EnterPasswordAsync(string password)
    {
        await _page.FillAsync("input[type='password']:nth-of-type(1)", password);
    }

    public async Task EnterPasswordAgainAsync(string password)
    {
        await _page.FillAsync("input[type='password']:nth-of-type(2)", password);
    }

    public async Task ClickSubmitAsync()
    {
        var selector = _page.GetByTestId("submit-button");
        await selector.ClickAsync();
    }

    // --- New methods for the note confirmation page ---
    public async Task ClickBackButtonAsync()
    {
        var selector = _page.GetByTestId("back-btn");
        await selector.ClickAsync();
    }

    public async Task<string> GetNoteLinkAsync()
    {
        var selector = _page.GetByTestId("note-link");
        return await selector.InnerTextAsync();
    }

    public async Task ClickNoteLinkAsync()
    {
        var selector = _page.GetByTestId("note-link");
        await selector.ClickAsync();
    }

    public async Task ClickCopyToClipboardAsync()
    {
        var selector = _page.GetByTestId("clipboard-btn");
        await selector.ClickAsync();
    }
}