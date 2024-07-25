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

        var mainSection = config.GetSection("AppSettings");
        
        if (mainSection == null)
            Console.WriteLine("Section do not exist!!");

        return mainSection!.Get<AppSettings>()!;
    }
}