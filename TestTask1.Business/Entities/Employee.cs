namespace TestTask1.Business.Entities;

public class Employee
{
    public int Identifier { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Patronymic { get; }
    public string Address { get; }
    public DateTime DateOfBirth { get; }
    public DateTime DateOfEmployment { get; }
    public string PhoneNumber { get; }
    public decimal Salary { get; }

    public int DepartmentIdentifier { get; }
    public int PositionIdentifier { get; }

    public Employee(
        int identifier,
        string firstName,
        string lastName,
        string patronymic,
        string address,
        DateTime dateOfBirth,
        DateTime dateOfEmployment,
        string phoneNumber,
        decimal salary,
        int departmentIdentifier,
        int positionIdentifier)
    {
        Identifier = identifier;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        Address = address;
        DateOfBirth = dateOfBirth;
        DateOfEmployment = dateOfEmployment;
        PhoneNumber = phoneNumber;
        Salary = salary;

        DepartmentIdentifier = departmentIdentifier;
        PositionIdentifier = positionIdentifier;
    }
}