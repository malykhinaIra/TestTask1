using System.Data;
using Microsoft.Data.SqlClient;
using TestTask1.Business.Entities;
using TestTask1.Business.Repositories;
using TestTask1.DataAccess.Database.Configuration;
using TestTask1.DataAccess.Database.Models;

namespace TestTask1.DataAccess.Database.Repositories;

public class PositionDatabaseRepository : IPositionRepository
{
    readonly string _connectionString;
    
    public PositionDatabaseRepository(DatabaseSettings databaseSettings)
    {
        _connectionString = databaseSettings.ConnectionString;
    }

    public async Task<Position[]> GetAllAsync()
    {
        var positions = new List<PositionModel>();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetAllPositions", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                positions.Add(new PositionModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    Title = result.GetString("Title")
                });
            }
        }

        return positions.Select(Map).ToArray();
    }
    
    public async Task<Position> GetOneAsync(int identifier)
    {
        var position = new PositionModel();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetPosition", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Identifier", identifier)
                }
            };

            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                position = new PositionModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    Title = result.GetString("Title")
                };
            }
        }

        return Map(position);
    }
    
    public async Task<Position[]> GetManyByDepartmentAsync(int departmentIdentifier)
    {
        var positions = new List<PositionModel>();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetPositionsByDepartment", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@DepartmentIdentifier", departmentIdentifier)
                }
            };

            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                positions.Add(new PositionModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    Title = result.GetString("Title")
                });
            }
        }

        return positions.Select(Map).ToArray();
    }

    static Position Map(PositionModel model)
    {
        return new Position(model.Identifier, model.Title);
    }
}