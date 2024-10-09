namespace TestTask1.DataAccess.Database.Configuration;

public class DatabaseSettings
{
    public string ConnectionString { get; }

    public DatabaseSettings(string connectionString)
    {
        ConnectionString = connectionString;
    }
}