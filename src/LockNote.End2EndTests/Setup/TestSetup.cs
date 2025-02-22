using Microsoft.Extensions.DependencyInjection;

namespace LockNote.End2EndTests.Setup;

public class TestSetup
{
    private static IServiceProvider? ServiceProvider { get; set; }

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        var exitCode = Microsoft.Playwright.Program.Main(["install"]);
        
        if(exitCode != 0)
        {
            throw new Exception("Failed to install Playwright");
        }
        
        var services = new ServiceCollection();

        // Register additional services
        services.AddSingleton<IConfigService, ConfigService>();

        // Build Service Provider
        ServiceProvider = services.BuildServiceProvider();
    }
    
    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
