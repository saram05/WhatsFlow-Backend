namespace WhatsFlow.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } // Admin / Supervisor / Agente

        public ICollection<User> Users { get; private set; } = new List<User>();

        private Role() { }

        public static Role Create(string name) => new Role { Id = Guid.NewGuid(), Name = name };
    }
}
