namespace TestTask1.DataAccess.Database.Configuration;

public class Settings
{
    public string ConnectionString { get; }

    public Settings(string connectionString)
    {
        ConnectionString = connectionString;
    }
}