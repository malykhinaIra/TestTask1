namespace TestTask1.Business.Entities;

public class Department
{
    public int Identifier { get; }
    public string Name { get; }
    
    public Department(int identifier, string name)
    {
        Identifier = identifier;
        Name = name;
    }
}