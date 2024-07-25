using Launcher;
using Launcher.Model;
using Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RollingFileWriter;
using IConfiguration = Launcher.IConfiguration;

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddSingleton<IRollingFileWriterService, RollingFileWriterService>()
    .AddSingleton<IMappingService, MappingService>()
    .AddSingleton<ICommonLogic, CommonLogic>()
    .AddScoped<IConfiguration, Configuration>()
    .BuildServiceProvider();
    
//configure console logging
serviceProvider
    .GetService<ILoggerFactory>();
    
var logger = serviceProvider.GetService<ILoggerFactory>()!
    .CreateLogger<Program>();
    
logger.LogDebug("Starting application");

var service = serviceProvider.GetService<ICommonLogic>();
service.Perform();