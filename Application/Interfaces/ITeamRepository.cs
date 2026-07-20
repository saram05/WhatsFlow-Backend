using WhatsFlow.Domain.Entities;

namespace WhatsFlow.Application.Interfaces;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(int id);
    Task<List<Team>> GetAllAsync();
    Task AddAsync(Team team);
}
