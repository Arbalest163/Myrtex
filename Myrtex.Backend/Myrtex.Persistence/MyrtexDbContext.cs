using Microsoft.EntityFrameworkCore;
using Myrtex.Application.Interfaces;
using Myrtex.Domain;

namespace Myrtex.Persistence;

public class MyrtexDbContext : DbContext, IMyrtexDbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<InformationEmployment> InformationEmployments => Set<InformationEmployment>();

    public DbSet<Department> Departments => Set<Department>();

    public MyrtexDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyrtexDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
