using BusinessLogic;
using BusinessLogic.Contracts;
using Common;
using Domain.Entities;
using NSubstitute;
using Repository.Contracts;
using Services.Contracts;

namespace BusinessLogicUnitTests;
[TestClass]
public sealed class AuthBusinessLogicTests
{
    private IAuthRepository authRepositoryMock;
    private IJwtTokenService jwtTokenServiceMock;
    private IHashService hashServiceMock;

    private IAuthBusinessLogic testee;

    [TestInitialize]
    public void Setup()
    {
        authRepositoryMock = Substitute.For<IAuthRepository>();
        jwtTokenServiceMock = Substitute.For<IJwtTokenService>();
        hashServiceMock = Substitute.For<IHashService>();

        testee = new AuthBusinessLogic(authRepositoryMock, jwtTokenServiceMock, hashServiceMock);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task Authenticate_WhenParameterIsNull_ThrowsException()
    {
        await testee.Authenticate(null);
    }

    [TestMethod]
    public async Task Authenticate_CallsRepository()
    {
        var user = DomainEntities.User1();

        await testee.Authenticate(user);

        await authRepositoryMock.Received(1).AddUser(user);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task Login_WhenParameterIsNull_ThrowsException()
    {
        await testee.Login(null);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task Login_WhenPasswordIsIncorrect_ThrowsException()
    {
        hashServiceMock.GetSha256Hash(Constants.Email1).Returns(Constants.EmailHash1);
        authRepositoryMock.GetUserByEmail(Constants.EmailHash1).Returns(Task.FromResult(DomainEntities.HashedUser1()));
        hashServiceMock.VerifyPassword(Constants.Password1, Constants.PasswordHash1).Returns(false);

        await testee.Login(DomainEntities.Credentials1());
    }
}
