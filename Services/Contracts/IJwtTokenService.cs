using Domain.Entities;

namespace Services.Contracts;
public interface IJwtTokenService
{
    string GetToken(User user);
}

