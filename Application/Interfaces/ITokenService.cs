using WhatsFlow.Domain.Entities;

namespace WhatsFlow.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
