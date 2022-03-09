using System.ComponentModel.DataAnnotations;

namespace JwtWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        // relations
        public IEnumerable<UserRol> UserRol { get; set; }
    }
}
