using DataAccess.Entities;

namespace Repository.Contracts.Mappers;
public interface IUserMapper
{
    User GetDataAccess(Domain.Entities.User user);
    Domain.Entities.User GetDomain(User user);
}
