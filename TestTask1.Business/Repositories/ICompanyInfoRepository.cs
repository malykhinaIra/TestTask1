using TestTask1.Business.Entities;

namespace TestTask1.Entities.Repositories;

public interface CompanyInfoRepository
{
    CompanyInfo GetOneAsync();
}