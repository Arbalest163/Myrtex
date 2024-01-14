namespace Myrtex.Domain;

public class InformationEmployment : Entity
{
    public Employee Employee { get; set; }
    public DateTime DateOfEmployment { get; set; }
}
