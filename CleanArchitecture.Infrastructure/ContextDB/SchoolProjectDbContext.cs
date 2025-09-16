using CleanArchitecture.Data.Entities.Entity;
using CleanArchitecture.Data.Entities.Identities;
using CleanArchitecture.Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Infrastructure.ContextDB
{
    public  class SchoolProjectDbContext : IdentityDbContext<UserApp,RoleApp,string>
    {
       
        public SchoolProjectDbContext(DbContextOptions<SchoolProjectDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentEntityConfigurations());
            modelBuilder.ApplyConfiguration(new SubjectEntityConfigurations());
            modelBuilder.ApplyConfiguration(new StudentEntityConfigurations());

            // we are writing FluentApi
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<DepartMent> DepartMents { get; set; }

        public virtual DbSet<StudentSubject> StudentSubjects { get; set; }

        public virtual DbSet<DepartMentSubject> DepartMentSubjects { get; set; }
    }
}
