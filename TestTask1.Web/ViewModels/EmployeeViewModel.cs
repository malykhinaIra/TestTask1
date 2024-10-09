namespace TestTask1.Web.ViewModels;

public class EmployeeViewModel
{
    public int Identifier { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Patronymic { get; init; }
    public string Address { get; init; }
    public DateTime DateOfBirth { get; init; }
    public DateTime DateOfEmployment { get; init; }
    public string PhoneNumber { get; init; }
    public decimal Salary { get; init; }

    public int DepartmentIdentifier { get; init; }
    public int PositionIdentifier { get; init; }
}