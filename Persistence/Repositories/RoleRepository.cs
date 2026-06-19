using WhatsFlow.Application.Interfaces;
using WhatsFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WhatsFlow.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByIdAsync(Guid id)
            => await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Role?> GetByNameAsync(string name)
            => await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);

        public async Task<IEnumerable<Role>> GetAllAsync()
            => await _context.Roles.ToListAsync();
    }
}
