using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Myrtex.Application.Common.Models;
using Myrtex.Application.Interfaces;
using System.Data;

namespace Myrtex.Application.Employees.Queries.GetEmployees;

public class GetEmployesQueryHandler : IRequestHandler<GetEmployesQuery, PageResponse<EmployeeView>>
{
    private readonly IMyrtexDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetEmployesQueryHandler(IMyrtexDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<PageResponse<EmployeeView>> Handle(GetEmployesQuery request, CancellationToken cancellationToken)
    {
        
        var query = _dbContext.Employees.AsQueryable();

        ApplyFilter();

        var skipPagesCount = Math.Max(0, request.Page - 1);
            
        var employees = await query
            .Skip(skipPagesCount * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<EmployeeView>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
        var totalCount = await query.CountAsync(cancellationToken);

        return new PageResponse<EmployeeView>
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
            TotalItems = totalCount,
            Items = employees,
        };

        void ApplyFilter()
        {
            var searchText = request.Filter.SearchInfo.SearchText;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = request.Filter.SearchInfo.SearchField switch
                {
                    FilterField.Department => query.Where(x => x.Department.Name.Contains(searchText)),
                    FilterField.FirstName => query.Where(x => x.FirstName.Contains(searchText)),
                    FilterField.LastName => query.Where(x => x.LastName.Contains(searchText)),
                    FilterField.MiddleName => query.Where(x => x.MiddleName.Contains(searchText)),
                    FilterField.BirthDate => DateTime.TryParse(searchText, out var date) ? query.Where(x => x.BirthDate == date) : query,
                    FilterField.DateOfEmployment => DateTime.TryParse(searchText, out var date) ?  query.Where(x => x.InformationEmployment.DateOfEmployment == date) : query,
                    FilterField.Salary => Decimal.TryParse(searchText, out var salary) ? query.Where(x => x.Salary == salary) : query,
                    _ => query,
                };
            }

            var ascending = request.Filter.OrderInfo.Ascending;

            query = request.Filter.OrderInfo.OrderField switch
            {
                FilterField.Department => ascending ? query.OrderBy(x => x.Department.Name) : query.OrderByDescending(x => x.Department.Name),
                FilterField.FirstName => ascending ? query.OrderBy(x => x.FirstName) : query.OrderByDescending(x => x.FirstName),
                FilterField.LastName => ascending ? query.OrderBy(x => x.LastName) : query.OrderByDescending(x => x.LastName),
                FilterField.MiddleName => ascending ? query.OrderBy(x => x.MiddleName) : query.OrderByDescending(x => x.MiddleName),
                FilterField.BirthDate => ascending ? query.OrderBy(x => x.BirthDate) : query.OrderByDescending(x => x.BirthDate),
                FilterField.DateOfEmployment => ascending ? query.OrderBy(x => x.InformationEmployment.DateOfEmployment) : query.OrderByDescending(x => x.InformationEmployment.DateOfEmployment),
                FilterField.Salary => ascending ? query.OrderBy(x => x.Salary) : query.OrderByDescending(x => x.Salary),
                _ => query.OrderBy(x => x.Department)
            };
        }
    }
}
