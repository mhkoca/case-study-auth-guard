using CaseStudy.AuthGuard.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy.AuthGuard.API.Models.DbContexts
{
    public class CaseStudyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        //public CaseStudyDbContext(DbContextOptions<CaseStudyDbContext> options):base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AuthGuard.db");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    Email = "test@test.com",
                    FirstName = "Mahmut",
                    LastName = "Wayne",
                    Password = "cmKaQbB25Yj7qMcco3+tyazcjnMhuctOpV/Qv5/o7XI=",
                    UserSalt = "salt",
                    IsActive = true,
                    IsDeleted = false
                }
               );

            base.OnModelCreating(modelBuilder);
        }
    }
}
