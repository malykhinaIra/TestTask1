using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using TestTask1.Business.Repositories;
using TestTask1.Business.Storages;
using TestTask1.Business.ValueObjects;
using TestTask1.DataAccess.Database.Configuration;
using TestTask1.DataAccess.Database.Models;

namespace TestTask1.DataAccess.Database.Storages;

public class SalaryReportDatabaseStorage : ISalaryReportStorage
{
    readonly IEmployeeRepository _employees;

    readonly string _connectionString;

    public SalaryReportDatabaseStorage(IEmployeeRepository employees, DatabaseSettings databaseSettings)
    {
        _employees = employees;
        
        _connectionString = databaseSettings.ConnectionString;
    }
    
    public async Task<SalaryReport> GetReportAsync(int? departmentIdentifier = null, int? positionIdentifier = null)
    {
        var employees = await _employees.GetAllAsync();
        if (employees.IsNullOrEmpty())
            return null;
        
        var report = new SalaryReportModel();

        await using (SqlConnection connection = new(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("Accounting.GetSalaryReport", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@DepartmentIdentifier", departmentIdentifier),
                    new SqlParameter("@PositionIdentifier", positionIdentifier)
                }
            };

            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                report = new SalaryReportModel
                {
                    TotalSalary = result.GetDecimal("TotalSalary"),
                    AverageSalary = result.GetDecimal("AverageSalary"),
                    MinSalary = result.GetDecimal("MinSalary"),
                    MaxSalary = result.GetDecimal("MaxSalary")
                };
            }
        }

        return new SalaryReport(report.TotalSalary, report.AverageSalary, report.MaxSalary, report.MinSalary);
    }
}