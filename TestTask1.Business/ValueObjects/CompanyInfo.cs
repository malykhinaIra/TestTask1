namespace TestTask1.Business.ValueObjects;

public class CompanyInfo
{
    public string Name { get; }
    public string Address { get; }
    public string PhoneNumber { get; }
    
    public CompanyInfo(string name, string address, string phoneNumber)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}