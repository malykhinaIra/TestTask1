using TestTask1.Business.Entities;
using TestTask1.Business.Repositories;

namespace TestTask1.DataAccess.Common.Repositories;

public class CompanyInfoCommonRepository : ICompanyInfoRepository
{
    public CompanyInfo GetOneAsync()
    {
        return new CompanyInfo("ТехноГруп", "вул. Хрещатик, 23", "+380978454130");
    }
}