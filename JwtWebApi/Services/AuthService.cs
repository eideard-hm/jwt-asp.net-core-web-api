using JwtWebApi.Context;
using JwtWebApi.Dto;
using JwtWebApi.Interfaces;
using JwtWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtWebApi.Services
{
    public class AuthService: IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context,
                            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> Register(User user)
        {
            if (UserExists(user.Username, user.Email)) return false;
            user.Password = PasswordHashBcrypt(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public User Login(LoginUserDto user)
        {
            var existsUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existsUser is null) return null;

            var validatePassword = verifyPassword(user.Password, existsUser.Password);
            if (!validatePassword) return null; 

            return existsUser;
        }


        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool verifyPassword(string userPassword, string dbPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userPassword, dbPassword);
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
