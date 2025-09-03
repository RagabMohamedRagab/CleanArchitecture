using CleanArchitecture.Data.Entities.Entity;
using CleanArchitecture.Infrastructure.DbSchemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.EntityConfigurations
{
    public class StudentEntityConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(nameof(Student), Schemas.Student);
        }
    }
}
