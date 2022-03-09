using JwtWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UserRol> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // definir la llave primaria (llave compuesta) en la relación Many-To-Many
            builder.Entity<UserRol>().HasKey(ur => new { ur.UserId, ur.RolId });

            // definir la relación o Foreign Key
            // User
            builder.Entity<UserRol>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.UserRol)
                .HasForeignKey(ur => ur.UserId);

            // definir la relación o Foreign Key
            // Rol
            builder.Entity<UserRol>()
                .HasOne<Rol>(ur => ur.Rol)
                .WithMany(r => r.UserRol)
                .HasForeignKey(ur => ur.RolId);
        }
    }
}
