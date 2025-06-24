using BusinessLogic.Contracts;
using Domain.Entities;
using Repository.Contracts;

namespace BusinessLogic;
public class AuthBusinessLogic : IAuthBusinessLogic
{
    private readonly IAuthRepository authRepository;

    public AuthBusinessLogic(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public Task Authenticate(User user)
    {
        throw new NotImplementedException();
    }
}
