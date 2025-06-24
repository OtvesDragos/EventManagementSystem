using DataAccess.Entities;
using Services;

namespace Repository.Mappers;
public static class UserMapper
{
    public static User GetDataAccess(Domain.Entities.User user)
    {
        return new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = HashService.GetPasswordHash(user.Password),
            Email = HashService.GetSha256Hash(user.Email)
        };
    }
}
