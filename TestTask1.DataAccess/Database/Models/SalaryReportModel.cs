namespace TestTask1.DataAccess.Database.Models;

public class SalaryReportModel
{
    public decimal TotalSalary { get; init; }
    public decimal AverageSalary { get; init; }
    public decimal MaxSalary { get; init; }
    public decimal MinSalary { get; init; }
}