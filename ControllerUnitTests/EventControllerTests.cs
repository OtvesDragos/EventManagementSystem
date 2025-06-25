using BusinessLogic.Contracts;
using Common;
using EventManagementSystem.Controllers;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Security.Claims;

namespace ControllerUnitTests;

[TestClass]
public class EventControllerTests
{
    private readonly CompareLogic compareLogic = new CompareLogic();

    private IEventBusinessLogic businessLogicMock;

    private EventController testee;

    [TestInitialize]
    public void Setup()
    {
        businessLogicMock = Substitute.For<IEventBusinessLogic>();

        testee = new EventController(businessLogicMock);
    }

    [TestMethod]
    public async Task Create_CallsBusinessLogic()
    {
        var event1 = DomainEntities.Event1();

        await testee.Create(event1);

        await businessLogicMock.Received(1).Create(event1);
    }

    [TestMethod]
    public async Task Edit_CallsBusinessLogic()
    {
        AddClaimsToTestee();
        var event1 = DomainEntities.Event1();

        await testee.Edit(event1);

        await businessLogicMock.Received(1).Edit(event1, Arg.Any<Guid>());
    }

    [TestMethod]
    public async Task Edit_WhenUserIdIsNotFound_ReturnsUnautorized()
    {
        var event1 = DomainEntities.Event1();

        var result = await testee.Edit(event1);

        Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
    }

    [TestMethod]
    public async Task Delete_CallsBusinessLogic()
    {
        AddClaimsToTestee();

        await testee.Delete(Constants.Code1);

        await businessLogicMock.Received(1).Delete(Constants.Code1, Arg.Any<Guid>());
    }

    [TestMethod]
    public async Task Delete_WhenUserIdIsNotFound_ReturnsUnautorized()
    {
        var result = await testee.Delete(Constants.Code1);

        Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
    }

    [TestMethod]
    public async Task GetAllByOwner_ReturnsExpected()
    {
        AddClaimsToTestee();
        var expected = DomainEntities.Events();
        businessLogicMock.GetAllByOwner(Constants.UserId1)
            .Returns(expected);

        var result = await testee.GetAllByOwner();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var actual = result as OkObjectResult;
        Assert.IsTrue(compareLogic.Compare(expected, actual.Value).AreEqual);
    }

    private void AddClaimsToTestee()
    {
        testee.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                 [new Claim("id", Constants.UserId1.ToString())]))
            }
        };
    }
}
