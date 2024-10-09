namespace TestTask1.Business.Entities;

public class Position
{
    public int Identifier { get; }
    public string Title { get; }
    
    public Position(int identifier, string title)
    {
        Identifier = identifier;
        Title = title;
    }
}