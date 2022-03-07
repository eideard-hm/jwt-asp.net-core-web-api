using JwtWebApi.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()) || string.IsNullOrWhiteSpace(id.ToString()))
                return BadRequest(new { status = false, mensaje = "Debe introducir un Id válido" });

            var isUserDeleted = await _userService.Delete(id);

            if (!isUserDeleted) return NotFound(new { status = false, message = "User not found" });

            return Ok(new {status = true, mensaje = "Usuario eliminado correctamente"});
        }

    }
}
