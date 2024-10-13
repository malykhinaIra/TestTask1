using AutoMapper;
using TestTask1.Business.Entities;
using TestTask1.Business.ValueObjects;
using TestTask1.Web.ViewModels;

namespace TestTask1.Web.Mappers;

public class ContractProfile : Profile
{
    public ContractProfile()
    {
        CreateMap<Position, PositionViewModel>();
        CreateMap<Department, DepartmentViewModel>();
        CreateMap<SalaryReport, SalaryReportViewModel>();
        CreateMap<CompanyInfo, CompanyInfoViewModel>();
        CreateMap<Employee, EmployeeViewModel>()
            .ReverseMap()
            .ConstructUsing(src => new Employee(
                src.Identifier,
                src.FirstName,
                src.LastName,
                src.Patronymic,
                src.Address,
                src.DateOfBirth,
                src.DateOfEmployment,
                src.PhoneNumber,
                src.Salary,
                src.DepartmentIdentifier,
                src.PositionIdentifier));
    }
}