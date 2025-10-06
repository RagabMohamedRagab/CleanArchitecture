using CleanArchitecture.Data.Entities.Entity;
using CleanArchitecture.Infrastructure.DbSchemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.EntityConfigurations
{
    public class DepartmentEntityConfigurations : IEntityTypeConfiguration<DepartMent>
    {
        public void Configure(EntityTypeBuilder<DepartMent> builder)
        {
            builder.ToTable(nameof(DepartMent), Schemas.Department);
        }
    }
}
