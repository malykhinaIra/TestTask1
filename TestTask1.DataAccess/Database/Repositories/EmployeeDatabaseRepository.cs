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

public class EmployeeDatabaseRepository : IEmployeeRepository
{
    readonly string _connectionString;

    public EmployeeDatabaseRepository(DatabaseSettings databaseSettings)
    {
        _connectionString = databaseSettings.ConnectionString;
    }

    public async Task UpdateAsync(Employee employee)
    {
        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.UpdateEmployee", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Identifier", employee.Identifier),
                    new SqlParameter("@FirstName", employee.FirstName),
                    new SqlParameter("@LastName", employee.LastName),
                    new SqlParameter("@Patronymic", employee.Patronymic),
                    new SqlParameter("@Address", employee.Address),
                    new SqlParameter("@DateOfBirth", employee.DateOfBirth),
                    new SqlParameter("@DateOfEmployment", employee.DateOfEmployment),
                    new SqlParameter("@PhoneNumber", employee.PhoneNumber),
                    new SqlParameter("@Salary", employee.Salary),
                    new SqlParameter("@DepartmentIdentifier", employee.DepartmentIdentifier),
                    new SqlParameter("@PositionIdentifier", employee.PositionIdentifier),
                }
            };

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<Employee[]> GetManyAsync(int? departmentIdentifier, int? positionIdentifier, string query)
    {
        var employees = new List<EmployeeModel>();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetEmployees", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@DepartmentIdentifier", departmentIdentifier),
                    new SqlParameter("@PositionIdentifier", positionIdentifier),
                    new SqlParameter("@SearchTerm", query),
                }
            };

            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                employees.Add(new EmployeeModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    FirstName = result.GetString("FirstName"),
                    LastName = result.GetString("LastName"),
                    Patronymic = result.GetString("Patronymic"),
                    Address = result.GetString("Address"),
                    DateOfBirth = result.GetDateTime("DateOfBirth"),
                    DateOfEmployment = result.GetDateTime("DateOfEmployment"),
                    PhoneNumber = result.GetString("PhoneNumber"),
                    Salary = result.GetDecimal("Salary"),
                    DepartmentIdentifier = result.GetInt32("DepartmentIdentifier"),
                    PositionIdentifier = result.GetInt32("PositionIdentifier")
                });
            }
        }

        return employees.Select(Map).ToArray();
    }

    public async Task<Employee[]> GetAllAsync()
    {
        var employees = new List<EmployeeModel>();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetEmployees", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                employees.Add(new EmployeeModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    FirstName = result.GetString("FirstName"),
                    LastName = result.GetString("LastName"),
                    Patronymic = result.GetString("Patronymic"),
                    Address = result.GetString("Address"),
                    DateOfBirth = result.GetDateTime("DateOfBirth"),
                    DateOfEmployment = result.GetDateTime("DateOfEmployment"),
                    PhoneNumber = result.GetString("PhoneNumber"),
                    Salary = result.GetDecimal("Salary"),
                    DepartmentIdentifier = result.GetInt32("DepartmentIdentifier"),
                    PositionIdentifier = result.GetInt32("PositionIdentifier")
                });
            }
        }

        return employees.Select(Map).ToArray();
    }

    public async Task<Employee> GetOneAsync(int identifier)
    {
        var employee = new EmployeeModel();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetEmployee", connection)
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
                employee = new EmployeeModel
                {
                    Identifier = result.GetInt32("Identifier"),
                    FirstName = result.GetString("FirstName"),
                    LastName = result.GetString("LastName"),
                    Patronymic = result.GetString("Patronymic"),
                    Address = result.GetString("Address"),
                    DateOfBirth = result.GetDateTime("DateOfBirth"),
                    DateOfEmployment = result.GetDateTime("DateOfEmployment"),
                    PhoneNumber = result.GetString("PhoneNumber"),
                    Salary = result.GetDecimal("Salary"),
                    DepartmentIdentifier = result.GetInt32("DepartmentIdentifier"),
                    PositionIdentifier = result.GetInt32("PositionIdentifier")
                };
            }
        }

        return Map(employee);
    }

    static Employee Map(EmployeeModel model)
    {
        return new Employee(
            model.Identifier,
            model.FirstName,
            model.LastName,
            model.Patronymic,
            model.Address,
            model.DateOfBirth,
            model.DateOfEmployment,
            model.PhoneNumber,
            model.Salary,
            model.DepartmentIdentifier,
            model.PositionIdentifier);
    }
}