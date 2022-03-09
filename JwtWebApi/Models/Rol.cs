namespace JwtWebApi.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // relations
        public IEnumerable<UserRol> UserRol { get; set; }
    }
}
