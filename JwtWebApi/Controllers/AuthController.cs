using JwtWebApi.Context;
using JwtWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (user == null || !ModelState.IsValid) return BadRequest();

            if(UserExists(user.Username, user.Email)) return BadRequest("El usuario con ese username y correo ya existen.");
            
            user.Password = PasswordHashBcrypt(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("user", new { id = user.Id, token = CreateToken(user) }, user);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool verifyPassword(string userPassword, string dbPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userPassword, userPassword);
        }

        private string PasswordHashBcrypt(string currentPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(currentPassword);
        }

        private bool UserExists(string username, string email)
        {
            return _context.Users.Any(e => e.Username == username && e.Email == email);
        }
    }
}
