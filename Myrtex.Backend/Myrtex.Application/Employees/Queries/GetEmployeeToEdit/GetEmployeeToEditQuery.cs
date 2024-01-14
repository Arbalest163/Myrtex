using MediatR;

namespace Myrtex.Application.Employees.Queries.GetEmployeeToEdit;

public class GetEmployeeToEditQuery : IRequest<EmployeeToEditView>
{
    public int Id { get; set; }
}
