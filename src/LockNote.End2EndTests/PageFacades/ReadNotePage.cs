namespace LockNote.End2EndTests.PageFacades;

using System.Threading.Tasks;
using Microsoft.Playwright;

public class ReadNotePage(IPage page) : PlaywrightFacade(page, "/")
{
    private readonly IPage _page = page;
    
    public async Task<string> GetMessageAsync()
    {
        await _page.PauseAsync();
        var selector = _page.GetByTestId("message-read");
        return await selector.InputValueAsync();
    }
}