namespace CaseStudy.AuthGuard.API.Models.DTOs
{
    public class UserLoginDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserSalt { get; set; }
    }
}
