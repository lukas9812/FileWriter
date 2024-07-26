using System.Reflection;
using System.Xml.Serialization;
using Launcher.Model;

namespace DataProcess;

public class DataProcessingService : IDataProcessingService
{
    public Person GetRandomPerson()
    {
        var people = GetData();
        var random = new Random();
        var randomIndex = random.Next(people.Count);
        var person = people[randomIndex];
        return person;
    }
    
    private List<Person> GetData()
    {
        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var binDirectory = Path.GetDirectoryName(assemblyPath);
        var path = Path.Combine(binDirectory!, "PersonData.xml");
        
        var xmlData = File.ReadAllText(path);
        var serializer = new XmlSerializer(typeof(List<Person>), new XmlRootAttribute("Persons"));
        using var reader = new StringReader(xmlData);
        return (List<Person>)serializer.Deserialize(reader)!;
    }
}