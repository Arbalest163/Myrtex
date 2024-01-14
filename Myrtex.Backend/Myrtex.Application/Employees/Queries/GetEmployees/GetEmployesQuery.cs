using MediatR;
using Myrtex.Application.Common.Models;

namespace Myrtex.Application.Employees.Queries.GetEmployees;

public class GetEmployesQuery : IRequest<PageResponse<EmployeeView>>
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;
}
