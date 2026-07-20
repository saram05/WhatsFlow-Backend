namespace WhatsFlow.Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Relación N:N con User
    public ICollection<User> Users { get; set; } = new List<User>();
}
