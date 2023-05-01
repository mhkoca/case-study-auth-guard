using CaseStudy.Data;
using CaseStudy.Data.DbContexts;
using CaseStudy.Employee.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy.Employee.API.Repositories
{
    public class EmployeeRepository : Repository<EmployeeEntity>
    {
        public EmployeeRepository() : base(new EmployeeDbContext())
        {
        }
    }
}
