using MediatR;
using Myrtex.Application.Common.Models;

namespace Myrtex.Application.Employees.Queries.GetEmployees;

public class GetEmployesQuery : IRequest<PageResponse<EmployeeView>>
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;
    public FilterContext Filter { get; set; } = new FilterContext();
}

public class FilterContext
{
    public OrderInfo OrderInfo { get; set; } = new OrderInfo();
    public SearchInfo SearchInfo { get; set; } = new SearchInfo();
}

public enum FilterField
{
    Department,
    LastName,
    FirstName,
    MiddleName,
    BirthDate,
    DateOfEmployment,
    Salary,
}

public class OrderInfo
{
    public FilterField OrderField { get; set; } = FilterField.Department;
    public bool Ascending { get; set; } = true;
}

public class SearchInfo
{
    public FilterField SearchField { get; set; } = FilterField.Department;
    public string? SearchText { get; set; } = string.Empty;
}
