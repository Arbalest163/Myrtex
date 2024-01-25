using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Myrtex.Application.Interfaces;

namespace Myrtex.Application.Departments.Queries.GetDepartments;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, DepartmentsView>
{
    private readonly IMyrtexDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetDepartmentsQueryHandler(IMyrtexDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<DepartmentsView> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _dbContext.Departments
            .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return new DepartmentsView
        {
            Departments = departments
        };
    }
}
