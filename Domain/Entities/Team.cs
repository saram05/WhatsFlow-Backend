namespace WhatsFlow.Domain.Entities
{
    public class Team
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public ICollection<User> Members { get; private set; } = new List<User>();

        private Team() { }

        public static Team Create(string name) => new Team { Id = Guid.NewGuid(), Name = name };
    }
}
