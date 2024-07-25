using Launcher.Model;
using Microsoft.Extensions.Configuration;

namespace Launcher;

public class Configuration : IConfiguration
{
    private AppSettings _config;

    public Configuration()
    {
        _config = GetAppSettings();
    }

    public AppSettings GetAppSettings()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        return config.GetSection("AppSettings").Get<AppSettings>();
    }
}