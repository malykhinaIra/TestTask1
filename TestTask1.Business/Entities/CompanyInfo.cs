namespace TestTask1.Business.Entities;

public class CompanyInfo
{
    public int Identifier { get; }
    public string Name { get; }
    public string Address { get; }
    public string PhoneNumber { get; }
    
    public CompanyInfo(int identifier, string name, string address, string phoneNumber)
    {
        Identifier = identifier;
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}