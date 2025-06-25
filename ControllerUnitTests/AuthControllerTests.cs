using BusinessLogic.Contracts;
using Common;
using EventManagementSystem.Controllers;
using NSubstitute;

namespace ControllerUnitTests;

[TestClass]
public sealed class AuthControllerTests
{
    private IAuthBusinessLogic businessLogicMock;
    private AuthController testee;

    [TestInitialize]
    public void Setup()
    {
        businessLogicMock = Substitute.For<IAuthBusinessLogic>();

        testee = new AuthController(businessLogicMock);
    }

    [TestMethod]
    public async Task Authenticate_CallsBusinessLogic()
    {
        var user1 = DomainEntities.User1();

        await testee.Authenticate(user1);

        await businessLogicMock.Received(1).Authenticate(user1);
    }

    [TestMethod]
    public async Task Login_ReturnsExpected()
    {
        var credentials = DomainEntities.Credentials1();
        businessLogicMock.Login(credentials).Returns(Task.FromResult(Constants.Token));

        var actual = await testee.Login(credentials);

        Assert.AreEqual(Constants.Token, actual);
    }
}
