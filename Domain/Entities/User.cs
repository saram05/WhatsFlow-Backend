namespace WhatsFlow.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public bool IsActive { get; private set; }
        public Guid RoleId { get; private set; }

        public Role Role { get; private set; }
        public ICollection<Team> Teams { get; private set; } = new List<Team>();

        private User() { } 

        public static User Create(string name, string email, string passwordHash, Guid roleId)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                IsActive = true,
                RoleId = roleId
            };
        }

        public void Deactivate() => IsActive = false;
        public void ChangeRole(Guid roleId) => RoleId = roleId;
    }
}
