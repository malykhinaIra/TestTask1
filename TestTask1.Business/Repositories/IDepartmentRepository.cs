using TestTask1.Business.Entities;

namespace TestTask1.Business.Repositories;

public interface IDepartmentRepository
{
    Task<Department[]> GetAllAsync();
    Task<Department> GetOneAsync(int identifier);
}