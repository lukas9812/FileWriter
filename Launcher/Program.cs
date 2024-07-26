using DataProcess;
using Launcher.Interfaces;
using Launcher.Services;
using Mapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RollingFileWriter;
using IConfiguration = Launcher.Interfaces.IConfiguration;

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddSingleton<IRollingFileWriterService, RollingFileWriterService>()
    .AddSingleton<IMappingService, MappingService>()
    .AddSingleton<ICommonLogicService, CommonLogicService>()
    .AddSingleton<IDataProcessingService, DataProcessingService>()
    .AddScoped<IConfiguration, Configuration>()
    .BuildServiceProvider();
    
//configure console logging
serviceProvider
    .GetService<ILoggerFactory>();
    
var logger = serviceProvider.GetService<ILoggerFactory>()!
    .CreateLogger<Program>();
    
logger.LogDebug("Starting application");

var commonLogic = serviceProvider.GetService<ICommonLogicService>();
if (commonLogic is null)
{
    logger.LogError("Common logic service is null!");
    return;
}
    
commonLogic.Perform();