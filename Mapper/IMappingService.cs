namespace Mapper;

public interface IMappingService
{
    Dictionary<string, object?> FilterData(object xbciData);
}