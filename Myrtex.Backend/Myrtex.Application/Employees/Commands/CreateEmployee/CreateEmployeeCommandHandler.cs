using MediatR;
using Microsoft.EntityFrameworkCore;
using Myrtex.Application.Interfaces;
using Myrtex.Domain;

namespace Myrtex.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IMyrtexDbContext _dbContext;

    public CreateEmployeeCommandHandler(IMyrtexDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var department = await _dbContext.Departments.FirstAsync(x => x.Id == request.DepartmentId, cancellationToken);
        if(department is null)
        {
            throw new Exception("Ошибка создания сотрудника");
        }

        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            BirthDate = request.BirthDate,
            Salary = request.Salary,
            InformationEmployment = new InformationEmployment
            {
                DateOfEmployment = DateTime.Now,
            },
            Department = department,
        };

        await _dbContext.Employees.AddAsync(employee, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return employee.Id;
    }
}
