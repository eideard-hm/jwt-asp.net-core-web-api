using JwtWebApi.Dto;
using JwtWebApi.Models;

namespace JwtWebApi.Interfaces
{
    public interface IAuthService
    {
        User Login(LoginUserDto user);
        string CreateToken(User user);
        Task<bool> Register(User user);
    }
}
