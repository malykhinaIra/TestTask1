using TestTask1.Business.Storages;
using TestTask1.Business.ValueObjects;

namespace TestTask1.DataAccess.Common.Storages;

public class CompanyInfoCommonStorage : ICompanyInfoStorage
{
    public CompanyInfo GetCompanyInfo()
    {
        return new CompanyInfo("ТехноГруп", "вул. Хрещатик, 23", "+380978454130");
    }
}