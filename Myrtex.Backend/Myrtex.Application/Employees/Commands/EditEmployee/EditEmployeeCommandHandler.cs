using MediatR;
using Microsoft.EntityFrameworkCore;
using Myrtex.Application.Interfaces;

namespace Myrtex.Application.Employees.Commands.EditEmployee;

public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand>
{
    private readonly IMyrtexDbContext _dbContext;

    public EditEmployeeCommandHandler(IMyrtexDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _dbContext.Employees.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        var department = await _dbContext.Departments.Where(x => x.Id == request.DepartmentId).FirstOrDefaultAsync(cancellationToken);

        if (employee is null || department is null)
        {
            throw new Exception("Ошибка редатирования сотрудника");
        }

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.MiddleName = request.MidleName;
        employee.BirthDate = request.BirthDate;
        employee.Department = department;
        employee.Salary = request.Salary;

        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
