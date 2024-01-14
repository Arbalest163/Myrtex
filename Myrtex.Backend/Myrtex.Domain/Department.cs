namespace Myrtex.Domain;

public class Department : Entity
{
    public string Name { get; set; }
    public IList<Employee> Employees { get; set; } = new List<Employee>();
}
