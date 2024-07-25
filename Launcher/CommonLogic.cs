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
        if (_configuration.GetAppSettings()!.IsMappingEnabled)
            _mappingService.FilterData("");
    }
    
    
}