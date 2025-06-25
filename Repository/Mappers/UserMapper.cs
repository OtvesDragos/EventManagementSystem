using DataAccess.Entities;
using Repository.Contracts.Mappers;
using Services.Contracts;

namespace Repository.Mappers;
public class UserMapper : IMapper<Domain.Entities.User, User>
{
    private readonly IHashService hashService;

    public UserMapper(IHashService hashService)
    {
        this.hashService = hashService;
    }

    public User GetDataAccess(Domain.Entities.User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = hashService.GetPasswordHash(user.Password),
            EmailHash = hashService.GetSha256Hash(user.Email)
        };
    }

    public Domain.Entities.User GetDomain(User user)
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
