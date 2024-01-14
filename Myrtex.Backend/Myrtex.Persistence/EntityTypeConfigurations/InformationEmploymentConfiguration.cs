using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Myrtex.Domain;

namespace Myrtex.Persistence.EntityTypeConfigurations;

public class InformationEmploymentConfiguration : IEntityTypeConfiguration<InformationEmployment>
{
    public void Configure(EntityTypeBuilder<InformationEmployment> builder)
    {
        builder.Property(x => x.DateOfEmployment).HasColumnType("date");
    }
}
