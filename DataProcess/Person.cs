using System.Reflection.Metadata.Ecma335;

namespace Launcher.Model;

public class Person
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public GenderType Gender { get; set; }
    
    public string Address { get; set; }  = string.Empty;
    
    public string JobPosition { get; set; } = string.Empty;
    
    public short Age { get; set; }
}

public enum GenderType
{
    Male,
    Female
}