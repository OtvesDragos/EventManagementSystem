using Common;
using DataAccess;
using Domain.Entities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Repository;
using Repository.Contracts;
using Repository.Contracts.Mappers;

namespace RepositoryUnitTests;

[TestClass]
public class EventRepositoryTests
{
    private readonly CompareLogic compareLogic = new CompareLogic();

    private IMapper<Event, DataAccess.Entities.Event> eventMapperMock;
    private DataContext dataContextMock;

    private IEventRepository testee;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dataContextMock = new DataContext(options);
        eventMapperMock = Substitute.For<IMapper<Event, DataAccess.Entities.Event>>();

        testee = new EventRepository(eventMapperMock, dataContextMock);
    }

    [TestCleanup]
    public void CleanUp()
    {
        dataContextMock.Dispose();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task Create_WhenParameterIsNull_ThrowsException()
    {
        await testee.Create(null);
    }

    [TestMethod]
    public async Task Create_AddsToDatabase()
    {
        var domainEvent = DomainEntities.Event1();
        var expected = DataAccessEntities.Event1();
        eventMapperMock.GetDataAccess(domainEvent).Returns(expected);

        await testee.Create(domainEvent);

        var actual = await dataContextMock.Events.FindAsync(expected.Id);

        Assert.IsTrue(compareLogic.Compare(expected, actual).AreEqual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetAllByOwner_WhenParameterIsDefault_ThrowsException()
    {
        await testee.GetAllByOwner(default);
    }



    [TestMethod]
    public async Task GetAllByOwner_ReturnsExpected()
    {
        dataContextMock.Events.AddRange(DataAccessEntities.Events());
        dataContextMock.SaveChanges();
        var domainEvent1 = DomainEntities.Event1();
        var domainEvent2 = DomainEntities.Event2();
        eventMapperMock.GetDomain(Arg.Is<DataAccess.Entities.Event>(x => x.Id == Constants.Id1))
            .Returns(domainEvent1);
        eventMapperMock.GetDomain(Arg.Is<DataAccess.Entities.Event>(x => x.Id == Constants.Id2))
            .Returns(domainEvent2);
        var expected = new List<Event> { domainEvent1, domainEvent2 };

        var actual = await testee.GetAllByOwner(Constants.UserId1);

        Assert.IsTrue(compareLogic.Compare(actual, expected).AreEqual);
    }
}
