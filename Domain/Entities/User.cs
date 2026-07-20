namespace WhatsFlow.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // FK a Role
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    // Relación N:N con Team
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}
