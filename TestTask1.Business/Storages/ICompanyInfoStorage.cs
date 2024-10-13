using TestTask1.Business.ValueObjects;

namespace TestTask1.Business.Storages;

public interface ICompanyInfoStorage
{
    CompanyInfo GetCompanyInfo();
}