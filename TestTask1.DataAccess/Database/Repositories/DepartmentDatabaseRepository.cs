using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TestTask1.Business.Entities;
using TestTask1.Business.Repositories;
using TestTask1.DataAccess.Database.Configuration;
using TestTask1.DataAccess.Database.Models;

namespace TestTask1.DataAccess.Database.Repositories;

public class DepartmentDatabaseRepository : IDepartmentRepository
{
    readonly string _connectionString;

    public DepartmentDatabaseRepository(DatabaseSettings databaseSettings)
    {
        _connectionString = databaseSettings.ConnectionString;
    }

    public async Task<Department[]> GetAllAsync()
    {
        var departments = new List<DepartmentModel>();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetAllDepartments", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                departments.Add(new DepartmentModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    Name = result.GetString("Name")
                });
            }
        }

        return departments.Select(Map).ToArray();
    }
    
    public async Task<Department[]> GetManyByPositionAsync(int positionIdentifier)
    {
        var departments = new List<DepartmentModel>();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetDepartmentsByPosition", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@PositionIdentifier", positionIdentifier)
                }
            };

            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                departments.Add(new DepartmentModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    Name = result.GetString("Name")
                });
            }
        }

        return departments.Select(Map).ToArray();
    }

    static Department Map(DepartmentModel model)
    {
        return new Department(model.Identifier, model.Name);
    }
}