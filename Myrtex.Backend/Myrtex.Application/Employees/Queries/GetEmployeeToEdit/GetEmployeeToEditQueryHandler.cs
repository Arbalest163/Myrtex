using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Myrtex.Application.Interfaces;

namespace Myrtex.Application.Employees.Queries.GetEmployeeToEdit;

public class GetEmployeeToEditQueryHandler : IRequestHandler<GetEmployeeToEditQuery, EmployeeToEditView>
{
    private readonly IMyrtexDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetEmployeeToEditQueryHandler(IMyrtexDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<EmployeeToEditView> Handle(GetEmployeeToEditQuery request, CancellationToken cancellationToken)
    {
        var employeeView = await _dbContext.Employees
            .Where(e => e.Id == request.Id)
            .ProjectTo<EmployeeToEditView>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if(employeeView is null)
        {
            throw new Exception("Сотрудник не найден"); //TODO Создать кастомные исключения.
        }

        return employeeView;
    }
}
