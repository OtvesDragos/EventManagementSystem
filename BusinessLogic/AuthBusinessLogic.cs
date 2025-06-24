using BusinessLogic.Contracts;
using Domain.Entities;
using Repository.Contracts;
using Services;

namespace BusinessLogic;
public class AuthBusinessLogic : IAuthBusinessLogic
{
    private readonly IAuthRepository authRepository;

    public AuthBusinessLogic(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public async Task Authenticate(User user)
    {
        await authRepository.AddUser(user);
    }
}
