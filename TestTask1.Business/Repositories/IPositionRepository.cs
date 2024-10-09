using TestTask1.Business.Entities;

namespace TestTask1.Business.Repositories;

public interface IPositionRepository
{
    Task<Position[]> GetAllAsync();
    Task<Position> GetOneAsync(int identifier);
    Task<Position[]> GetManyByDepartmentAsync(int departmentIdentifier);
}