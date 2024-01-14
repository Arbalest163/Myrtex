using Microsoft.EntityFrameworkCore;
using Myrtex.Domain;

namespace Myrtex.Application.Interfaces;

public interface IMyrtexDbContext
{
    DbSet<User> Users { get; }
    DbSet<Employee> Employees { get; }
    DbSet<InformationEmployment> InformationEmployments { get; }
    DbSet<Department> Departments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
