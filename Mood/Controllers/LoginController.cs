using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mood.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mood.Controllers {
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private IConfiguration _config;
        private readonly MoodDBContext _db;

        public LoginController(MoodDBContext context, IConfiguration config) {
            _db = context;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin) {
            var user = Authenticate(userLogin);

            if (user != null) {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string Generate(TblUser user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName.Trim()),
                new Claim(ClaimTypes.Email, user.EmailAddress.Trim()),
                new Claim(ClaimTypes.GivenName, user.GivenName.Trim()),
                new Claim(ClaimTypes.Surname, user.Surname.Trim()),
                new Claim(ClaimTypes.Role, user.Role.Trim())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private TblUser Authenticate(UserLogin userLogin) {
            
            var currentUser = _db.TblUsers.FirstOrDefault(o => o.UserName.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

            if (currentUser != null) {
                return currentUser;
            }

            return null;
        }
    }
}
