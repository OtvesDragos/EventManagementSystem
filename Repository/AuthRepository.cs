using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.Contracts.Mappers;

namespace Repository;
public class AuthRepository : IAuthRepository
{
    private readonly IMapper<User, DataAccess.Entities.User> userMapper;
    private readonly DataContext dataContext;

    public AuthRepository(DataContext dataContext, IMapper<User, DataAccess.Entities.User> userMapper)
    {
        this.dataContext = dataContext;
        this.userMapper = userMapper;
    }

    public async Task AddUser(User user)
    {
        await dataContext.Users.AddAsync(userMapper.GetDataAccess(user));
        await dataContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(string emailHash)
    {
        if (string.IsNullOrWhiteSpace(emailHash))
        {
            throw new ArgumentException(nameof(emailHash));
        }

        var user = await dataContext.Users
            .FirstOrDefaultAsync(x => emailHash == x.EmailHash);

        if (user == null)
        {
            throw new InvalidOperationException("No user was found by email!");
        }

        return userMapper.GetDomain(user);
    }
}
