using EMS.APP.Model;
using Microsoft.EntityFrameworkCore;

namespace EMS.APP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name");

                entity.Property(e => e.Email)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnName("role");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at");

                modelBuilder.Entity<Employee>()
            .ToTable("employees");

                modelBuilder.Entity<Departments>(entity =>
                {
                    entity.ToTable("departments");

                    entity.HasKey(e => e.DepartmentId);
                    entity.Property(e => e.DepartmentId)
                     .HasColumnName("department_id");

                    entity.Property(e => e.DepartmentName)
                        .HasColumnName("department_name");

                    entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");
                });
                
                modelBuilder.Entity<Designation>(entity =>
                {
                    entity.ToTable("designations");

                    entity.HasKey(e => e.DesignationId);
                    entity.Property(e => e.DesignationId)
                     .HasColumnName("designation_id");

                    entity.Property(e => e.DesignationName)
                        .HasColumnName("designation_name");

                    entity.Property(e => e.DepartmentId)
                        .HasColumnName("department_id");

                    entity.Property(e => e.CreatedAt)
                        .HasColumnName("created_at");
                });
            });
        }
    }
}