using System.Threading.Tasks;
using TestTask1.Business.Entities;

namespace TestTask1.Business.Repositories;

public interface IEmployeeRepository
{
    Task UpdateAsync(Employee employee);
    Task<Employee[]> GetManyAsync(int? departmentIdentifier, int? positionIdentifier, string query);
    Task<Employee> GetOneAsync(int identifier);
    Task<Employee[]> GetAllAsync();
}