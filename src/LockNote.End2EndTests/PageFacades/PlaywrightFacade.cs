using LockNote.End2EndTests.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace LockNote.End2EndTests.PageFacades;

using System.Threading.Tasks;
using Microsoft.Playwright;

public abstract class PlaywrightFacade(IPage page): IPlaywrightFacade
{ 
    private readonly string _url = "/";
    private readonly IConfigService _configService = null!;
    

    protected PlaywrightFacade(IPage page, string url) : this(page)
    {
        _url = url;
        _configService = new ConfigService();
    }

    public async Task<PlaywrightFacade> GoToPageAsync()
    {
        await page.GotoAsync($"{_configService.GetBaseUrl()}{_url}");
        return this;
    }
    
}
