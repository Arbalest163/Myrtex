using MediatR;

namespace Myrtex.Application.Employees.Commands.EditEmployee;

public class EditEmployeeCommand : IRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MidleName { get; set; }
    public DateTime BirthDate { get; set; }
    public int DepartmentId { get; set; }
    public decimal Salary { get; set; }
}
