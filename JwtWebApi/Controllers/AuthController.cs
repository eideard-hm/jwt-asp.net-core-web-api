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
            if (user == null || !ModelState.IsValid) return BadRequest("Los datos ingresado son incorrectos.");

            if (!await _authService.Register(user)) return BadRequest("El usuario o el correo ya se encuentra registrado.");

            return StatusCode(201);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto user)
        {
            if (user == null || !ModelState.IsValid) return BadRequest("Los datos ingresado son incorrectos.");

            var userExists = _authService.Login(user);

            if (userExists == null) return BadRequest("El usuario o la contraseña son incorrectos");

            return Ok(new { user = userExists, toke = _authService.CreateToken(userExists) });
        }
    }
}
