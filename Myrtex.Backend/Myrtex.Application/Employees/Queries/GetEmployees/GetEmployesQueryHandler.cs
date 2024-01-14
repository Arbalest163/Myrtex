using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Myrtex.Application.Common.Models;
using Myrtex.Application.Interfaces;

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
        var skipPagesCount = Math.Max(0, request.Page - 1);
        var employees = await _dbContext.Employees
            .Skip(skipPagesCount * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<EmployeeView>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
        var totalCount = await _dbContext.Employees.CountAsync(cancellationToken);

        return new PageResponse<EmployeeView>
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
            TotalItems = totalCount,
            Items = employees,
        };
    }
}
