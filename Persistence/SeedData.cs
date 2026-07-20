using Microsoft.EntityFrameworkCore;
using WhatsFlow.Domain.Entities;

namespace WhatsFlow.Persistence;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Crear roles si no existen
        if (!await context.Roles.AnyAsync())
        {
            context.Roles.AddRange(
                new Role { Name = "Admin" },
                new Role { Name = "Supervisor" },
                new Role { Name = "Agent" }
            );
            await context.SaveChangesAsync();
        }

        // Crear usuario Admin si no existe
        if (!await context.Users.AnyAsync(u => u.Email == "admin@plataforma.com"))
        {
            var adminRole = await context.Roles.FirstAsync(r => r.Name == "Admin");

            context.Users.Add(new User
            {
                Name = "Administrador",
                Email = "admin@plataforma.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                RoleId = adminRole.Id,
                IsActive = true
            });
            await context.SaveChangesAsync();
        }
    }
}
