using Domain.Entities;

namespace Repository.Contracts;
public interface IAuthRepository
{
    Task AddUser(User user);
}
