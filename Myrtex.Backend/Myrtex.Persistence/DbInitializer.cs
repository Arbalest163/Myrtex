using Microsoft.EntityFrameworkCore;
using Myrtex.Domain;
using Myrtex.Persistence.Extensions;

namespace Myrtex.Persistence;

public class DbInitializer
{
    public static void Initialize(MyrtexDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Database.Migrate();

        var departnments = CreateDepartments().ToArray();
        context.Departments.AddRange(departnments);
        context.SaveChanges();

        var users = CreateUsers().ToArray();
        context.Users.AddRange(users);
        context.SaveChanges();

        IEnumerable<Department> CreateDepartments()
        {
            foreach (var i in 0..5)
            {
                yield return new Department
                {
                    Name = $"Departnment {i}",
                };
            }
        }

        IEnumerable<Employee> CreateUsers()
        {
            foreach(var i in 0..20)
            {
                yield return new Employee
                {
                    FirstName = $"FirstName {i}",
                    LastName = $"LastName {i}",
                    MiddleName = $"MiddleName {i}",
                    BirthDate = DateTime.Now,
                    Department = departnments.GetRandomElement(),
                    InformationEmployment = new InformationEmployment
                    {
                        DateOfEmployment = DateTime.Now,
                    },
                    Salary = i * 1000,
                };
            }
        }
    }

    
}
