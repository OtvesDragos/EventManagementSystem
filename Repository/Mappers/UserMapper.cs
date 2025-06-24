using DataAccess.Entities;
using Services;

namespace Repository.Mappers;
public static class UserMapper
{
    public static User GetDataAccess(Domain.Entities.User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = HashService.GetPasswordHash(user.Password),
            EmailHash = HashService.GetSha256Hash(user.Email)
        };
    }

    public static Domain.Entities.User GetDomain(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return new Domain.Entities.User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.PasswordHash,
            Email = user.EmailHash
        };
    }
}
