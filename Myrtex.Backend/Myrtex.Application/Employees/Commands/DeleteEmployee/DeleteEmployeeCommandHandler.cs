using MediatR;
using Microsoft.EntityFrameworkCore;
using Myrtex.Application.Interfaces;

namespace Myrtex.Application.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IMyrtexDbContext _dbContext;

    public DeleteEmployeeCommandHandler(IMyrtexDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _dbContext.Employees.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        if (employee is null)
        {
            throw new Exception("Ошибка удаления сотрудника");
        }

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
