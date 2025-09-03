using CleanArchitecture.Data.Entities.Entity;
using CleanArchitecture.Infrastructure.DbSchemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.EntityConfigurations
{
    public class SubjectEntityConfigurations : IEntityTypeConfiguration<Subject>
    {

        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable(nameof(Subject), Schemas.Subject);
        }
    }
}
