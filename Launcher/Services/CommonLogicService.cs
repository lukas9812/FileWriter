using DataProcess;
using Launcher.Interfaces;
using Mapper;
using Newtonsoft.Json;
using RollingFileWriter;

namespace Launcher.Services;

public class CommonLogicService : ICommonLogicService
{
    private readonly IRollingFileWriterService _rollingFileWriterService;
    private readonly IMappingService _mappingService;
    private readonly IConfiguration _configuration;
    private readonly IDataProcessingService _dataProcessingService;
    // ReSharper disable once ConvertToPrimaryConstructor
    public CommonLogicService(
    IRollingFileWriterService rollingFileWriterService, 
    IMappingService mappingService, 
    IConfiguration configuration, 
    IDataProcessingService dataProcessingService)
    {
        _rollingFileWriterService = rollingFileWriterService;
        _mappingService = mappingService;
        _configuration = configuration;
        _dataProcessingService = dataProcessingService;
    }

    public void Perform()
    {
        var person = _dataProcessingService.GetRandomPerson();

        string content;
        
        if (_configuration.Get().IsMappingEnabled)
        {
            var filteredData = _mappingService.FilterData(person);
            content = JsonConvert.SerializeObject(filteredData);
        }
        else
        {
            content = JsonConvert.SerializeObject(person);
        }
        
        //_rollingFileWriterService.WriteData(content);
        var tmp = new RollingFileWriterService("customDir", "customFilePath", 20);
        tmp.WriteData(content);
        Console.WriteLine($"Data for {person.LastName} were written down.");
        
        var dataAsObject = new List<string> { "test1", "test2", "test3", "test4" };
        var dict = new Dictionary<string, int>
        {
            {"key1", 1},
            {"key2", 2},
            {"key3", 3},
            {"key4", 4}
        };
        
        _rollingFileWriterService.WriteData(dataAsObject);
        //_rollingFileWriterService.WriteData(dict);
    }
}