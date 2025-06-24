using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.Mappers;
using Services;

namespace Repository;
public class AuthRepository : IAuthRepository
{
    private readonly DataContext dataContext;

    public AuthRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task AddUser(User user)
    {
        await dataContext.Users.AddAsync(UserMapper.GetDataAccess(user));
        await dataContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(Credentials credentials)
    {
        var emailHash = HashService.GetSha256Hash(credentials.Email);
        var user = await dataContext.Users
            .FirstOrDefaultAsync(x => emailHash == x.EmailHash);

        if (user == null)
        {
            throw new InvalidOperationException($"No user with email = {credentials.Email} was found!");
        }

        return UserMapper.GetDomain(user);
    }
}
