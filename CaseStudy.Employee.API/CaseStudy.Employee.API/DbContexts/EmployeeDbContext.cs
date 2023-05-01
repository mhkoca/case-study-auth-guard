using CaseStudy.Employee.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy.Data.DbContexts
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }

        //public CaseStudyDbContext(DbContextOptions<CaseStudyDbContext> options):base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Employee.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>().HasData(
                new EmployeeEntity
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    Email = "employee1@test.com",
                    FirstName = "Mahmut",
                    LastName = "Wayne",
                    Phone = "0555555555",
                    IsActive = true,
                    IsDeleted = false
                },
                new EmployeeEntity
                {
                    Id = 2,
                    CreatedDate = DateTime.Now,
                    Email = "employee2@test.com",
                    FirstName = "Makbule",
                    LastName = "Wayne",
                    Phone = "0555555556",
                    IsActive = true,
                    IsDeleted = false
                });

            base.OnModelCreating(modelBuilder); 
        }
    }
}
