using BusinessLogic.Contracts;
using Domain.Entities;
using Repository.Contracts;
using Services;
using Services.Contracts;

namespace BusinessLogic;
public class AuthBusinessLogic : IAuthBusinessLogic
{
    private readonly IAuthRepository authRepository;
    private readonly IJwtTokenService jwtTokenService;
    private readonly IHashService hashService;

    public AuthBusinessLogic(IAuthRepository authRepository, IJwtTokenService jwtTokenService, IHashService hashService)
    {
        this.authRepository = authRepository;
        this.jwtTokenService = jwtTokenService;
        this.hashService = hashService;
    }

    public async Task Authenticate(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await authRepository.AddUser(user);
    }

    public async Task<string> Login(Credentials credentials)
    {
        if (credentials == null)
        {
            throw new ArgumentNullException(nameof(credentials));
        }

        var user = await authRepository.GetUserByEmail(hashService.GetSha256Hash(credentials.Email));

        if (!hashService.VerifyPassword(credentials.Password, user.Password))
        {
            throw new InvalidOperationException("The password is incorrect!");
        }

        return jwtTokenService.GetToken(user);
    }
}
