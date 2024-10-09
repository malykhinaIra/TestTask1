using TestTask1.Business.Entities;

namespace TestTask1.Business.Repositories;

public interface ICompanyInfoRepository
{
    CompanyInfo GetOneAsync();
}