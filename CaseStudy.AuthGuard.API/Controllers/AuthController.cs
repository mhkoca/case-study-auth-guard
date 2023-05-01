using CaseStudy.AuthGuard.API.Handlers;
using CaseStudy.AuthGuard.API.Helpers;
using CaseStudy.AuthGuard.API.Models;
using CaseStudy.AuthGuard.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.AuthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepository _repository;
        private readonly ITokenHandler _tokenHandler;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ITokenHandler tokenHandler, AuthRepository repository, ILogger<AuthController> logger)
        {
            _tokenHandler = tokenHandler;
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var user = _repository.Get(x => x.Email == model.Email && x.IsActive && !x.IsDeleted).FirstOrDefault();

                if (user == null)
                    return Unauthorized();

                var hash = SecurityHelper.HashCreate(model.Password, user.UserSalt);
                if (string.IsNullOrEmpty(hash) || user.Password != hash)
                    return Unauthorized();

                return Ok(_tokenHandler.GenerateJSONWebToken(user.Id, user.Email));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                return new StatusCodeResult(500);
            }
        }

        [HttpGet("Authenticate")]
        [Authorize]
        public IActionResult Authenticate()
        {
            return Ok();
        }
    }
}
