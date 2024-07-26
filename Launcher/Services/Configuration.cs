using Launcher.Model;
using Microsoft.Extensions.Configuration;
using IConfiguration = Launcher.Interfaces.IConfiguration;

namespace Launcher.Services;

public class Configuration : IConfiguration
{
    private AppSettings _config;

    public Configuration()
    {
        _config = Get();
    }

    public AppSettings Get()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var mainSection = config.GetSection("AppSettings");

        return mainSection.Get<AppSettings>()!;
    }
}