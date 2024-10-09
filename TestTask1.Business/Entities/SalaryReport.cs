namespace TestTask1.Business.Entities;

public class SalaryReport
{
    public decimal TotalSalary { get; }
    public decimal AverageSalary { get; }
    public decimal MaxSalary { get; }
    public decimal MinSalary { get; }
    
    public SalaryReport(decimal totalSalary, decimal averageSalary, decimal maxSalary, decimal minSalary)
    {
        TotalSalary = totalSalary;
        AverageSalary = averageSalary;
        MaxSalary = maxSalary;
        MinSalary = minSalary;
    }
}