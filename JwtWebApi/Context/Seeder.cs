using JwtWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApi.Context
{
    public static class Seeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // sedder for User
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        Username = "ediard-hm",
                        Name = "Edier",
                        LastName = "Hernández",
                        Email = "edierhernandezmo@gmail.com",
                        Password = "123",
                        Token = "ddf"
                    },
                    new User
                    {
                        Id = 2,
                        Username = "ediard-hm",
                        Name = "Edier",
                        LastName = "Hernández",
                        Email = "edierhernandezmo@gmail.com",
                        Password = "123",
                        Token = "ddf"
                    },
                    new User
                    {
                        Id = 3,
                        Username = "yese",
                        Name = "Yesenia",
                        LastName = "Florez",
                        Email = "yesenia@gmail.com",
                        Password = "123",
                        Token = "ddf"
                    }
                );

            // sedder for Rol
            modelBuilder.Entity<Rol>()
                .HasData(
                    new Rol { Id = 1, Nombre = "Administrador" },
                    new Rol { Id = 2, Nombre = "Clerk" },
                    new Rol { Id = 3, Nombre = "Customer" }
                );

            // sedder for UserRol
            modelBuilder.Entity<UserRol>()
                .HasData(
                    new UserRol { UserId = 1, RolId = 1 },
                    new UserRol { UserId = 2, RolId = 2 },
                    new UserRol { UserId = 3, RolId = 3 }
                );
        }
    }
}
