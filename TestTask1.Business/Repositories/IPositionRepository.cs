using System.Threading.Tasks;
using TestTask1.Business.Entities;

namespace TestTask1.Business.Repositories;

public interface IPositionRepository
{
    Task<Position[]> GetAllAsync();
    Task<Position[]> GetManyByDepartmentAsync(int departmentIdentifier);
}