using System.Xml.Linq;

namespace Mapper;

public class MappingService : IMappingService
{
    private const string MAPPING_FILE_PATH = "../../../Mappings/Filter.xml";
    private readonly XDocument XDoc = XDocument.Load(MAPPING_FILE_PATH);

    /// <summary>
    ///Method allows filtering exact FieldNames written down in .xml file and map it to (PropertyName, PropertyValue) type.
    /// </summary>
    /// <param name="data">Collection data received from XONTRO</param>
    /// <returns></returns>
    public Dictionary<string, object?> FilterData(object data)
    {
        var filteredProperties = new Dictionary<string, object?>();

        var properties = data.GetType().GetProperties();
        var fields = GetFields().ToList();

        foreach (var propertyInfo in properties)
        {
            if (!fields.Contains(propertyInfo.Name))
                continue;

            var propertyValue = propertyInfo.GetValue(data);
            filteredProperties.Add(propertyInfo.Name, propertyValue);
        }

        return filteredProperties;
    }

    private IEnumerable<string> GetFields()
        => XDoc
            .Descendants("Field")
            .Select(item => (string)item.Attribute("FieldName")!)
            .ToList();
}