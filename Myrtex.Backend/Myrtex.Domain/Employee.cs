namespace Myrtex.Domain;

public class Employee : User
{
    public Department Department { get; set; }
    public InformationEmployment InformationEmployment { get; set; }
    public decimal Salary { get; set; }
}
