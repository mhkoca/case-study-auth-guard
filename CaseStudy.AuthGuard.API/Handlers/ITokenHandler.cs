namespace CaseStudy.AuthGuard.API.Handlers
{
    public interface ITokenHandler
    {
        string GenerateJSONWebToken(int id, string email);
    }
}