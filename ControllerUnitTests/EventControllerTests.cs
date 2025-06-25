using BusinessLogic.Contracts;
using Common;
using EventManagementSystem.Controllers;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;

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
        var event1 = DomainEntities.Event1();

        await testee.Edit(event1);

        await businessLogicMock.Received(1).Edit(event1);
    }

    [TestMethod]
    public async Task Delete_CallsBusinessLogic()
    {
        await testee.Delete(Constants.Code1);

        await businessLogicMock.Received(1).Delete(Constants.Code1);
    }

    [TestMethod]
    public async Task GetAllByOwner_ReturnsExpected()
    {
        var expected = DomainEntities.Events();
        businessLogicMock.GetAllByOwner(Constants.Email1)
            .Returns(expected);

        var actual = await testee.GetAllByOwner(Constants.Email1);

        Assert.IsTrue(compareLogic.Compare(expected, actual).AreEqual);
    }
}
