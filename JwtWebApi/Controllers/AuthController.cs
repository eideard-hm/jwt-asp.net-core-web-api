using JwtWebApi.Dto;
using JwtWebApi.Interfaces;
using JwtWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email)
                || string.IsNullOrEmpty(user.Password))
                return BadRequest(new { mensaje = "Los datos ingresado son incorrectos." });

            if (!await _authService.Register(user)) return BadRequest(new { mensaje = "El usuario o el correo ya se encuentra registrado." });

            return Ok(new { status = true, statusCode = StatusCode(201) });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return BadRequest(new { status = false,  mensaje = "Los datos ingresado son incorrectos." });

            var userExists = _authService.Login(user);

            if (userExists == null) return NotFound(new { status = false, mensaje = "El usuario o la contraseña son incorrectos" });

            return Ok(new { status = true,  user = userExists, token = _authService.CreateToken(userExists) });
        }
    }
}
