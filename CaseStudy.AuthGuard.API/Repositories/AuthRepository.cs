using CaseStudy.AuthGuard.API.Models.DbContexts;
using CaseStudy.AuthGuard.API.Models.Entities;
using CaseStudy.Data;

namespace CaseStudy.AuthGuard.API.Repositories
{
    public class AuthRepository : Repository<User>
    {
        public AuthRepository() : base(new CaseStudyDbContext())
        {
        }
    }
}
