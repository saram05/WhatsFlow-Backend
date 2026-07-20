using WhatsFlow.Domain.Entities;

namespace WhatsFlow.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);
    Task<Role?> GetByNameAsync(string name);
    Task<List<Role>> GetAllAsync();
}
