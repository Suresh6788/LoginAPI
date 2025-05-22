using LoginApi.Data;
using LoginApi.Models.UserLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly LoginDbContect _contect;

        public LoginController(IConfiguration configuration, LoginDbContect contect)
        {
            _configuration = configuration;
            _contect = contect;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var vlaidUser= _contect.Users.FirstOrDefault(x=>x.UserName == login.UserName && x.Password == login.Password);

            if (vlaidUser != null )
            {
                var token = GenerateJwtToken(login.UserName);
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid username or password");
        }
           
        private string GenerateJwtToken(string userName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

