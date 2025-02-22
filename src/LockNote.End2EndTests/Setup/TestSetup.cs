using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace LockNote.End2EndTests.Setup;

public class TestSetup
{
    private static IServiceProvider? ServiceProvider { get; set; }

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        var exitCode = Program.Main(new[] { "install" });
        if (exitCode != 0)
        {
            throw new InvalidOperationException($"Playwright exited with code {exitCode}");
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
