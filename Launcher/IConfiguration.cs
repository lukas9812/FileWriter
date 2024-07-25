using Launcher.Model;

namespace Launcher;

public interface IConfiguration
{
    AppSettings? GetAppSettings();
}