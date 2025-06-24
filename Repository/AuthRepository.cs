using DataAccess;
using Domain.Entities;
using Repository.Contracts;
using Repository.Mappers;

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
        await dataContext.AddAsync(UserMapper.GetDataAccess(user));
        await dataContext.SaveChangesAsync();
    }
}
