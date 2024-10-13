using System.Threading.Tasks;
using TestTask1.Business.ValueObjects;

namespace TestTask1.Business.Storages;

public interface ISalaryReportStorage
{
    Task<SalaryReport> GetReportAsync(int? departmentIdentifier = null, int? positionIdentifier = null);
}