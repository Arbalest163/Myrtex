using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Myrtex.Domain;

namespace Myrtex.Persistence.EntityTypeConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(nameof(Employee));

        builder.HasOne(x => x.InformationEmployment).WithOne(x => x.Employee).HasForeignKey<InformationEmployment>(x => x.Id);
    }
}
