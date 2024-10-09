using System.Data;
using Microsoft.Data.SqlClient;
using TestTask1.Business.Entities;
using TestTask1.Business.Repositories;
using TestTask1.DataAccess.Database.Configuration;
using TestTask1.DataAccess.Database.Models;

namespace TestTask1.DataAccess.Database.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    readonly string _connectionString;

    public DepartmentRepository(DatabaseSettings databaseSettings)
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
    
    public async Task<Department> GetOneAsync(int identifier)
    {
        var department = new DepartmentModel();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetDepartment", connection)
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
                department = new DepartmentModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    Name = result.GetString("Name")
                };
            }
        }

        return Map(department);
    }

    static Department Map(DepartmentModel model)
    {
        return new Department(model.Identifier, model.Name);
    }
}