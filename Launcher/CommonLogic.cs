using Mapper;
using RollingFileWriter;

namespace Launcher;

public class CommonLogic : ICommonLogic
{
    private readonly IRollingFileWriterService _rollingFileWriterService;
    private readonly IMappingService _mappingService;
    private readonly IConfiguration _configuration;

    public CommonLogic(IRollingFileWriterService rollingFileWriterService, IMappingService mappingService, IConfiguration configuration)
    {
        _rollingFileWriterService = rollingFileWriterService;
        _mappingService = mappingService;
        _configuration = configuration;
    }

    public void Perform()
    {
        _rollingFileWriterService.WriteData("kkte");
        _mappingService.FilterData("kokote");
        var cnf = _configuration.GetAppSettings();
        var isMappingEnabled = cnf.IsMappingEnabled;
        Console.Write(isMappingEnabled);
    }
    
    
}