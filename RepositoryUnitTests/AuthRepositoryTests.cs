using Common;
using DataAccess;
using DataAccess.Entities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Repository;
using Repository.Contracts;
using Repository.Contracts.Mappers;

namespace RepositoryUnitTests;
[TestClass]
public sealed class AuthRepositoryTests
{
    private readonly CompareLogic compareLogic = new CompareLogic();
    private IMapper<Domain.Entities.User, User> userMapperMock;
    private DataContext dataContextMock;

    private IAuthRepository testee;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dataContextMock = new DataContext(options);
        userMapperMock = Substitute.For<IMapper<Domain.Entities.User, User>>();

        testee = new AuthRepository(dataContextMock, userMapperMock);
    }

    [TestCleanup]
    public void CleanUp()
    {
        dataContextMock.Dispose();
    }

    [TestMethod]
    public async Task AddUser_AddsMappedUserToDatabase()
    {
        var domainUser = DomainEntities.User1();
        var dataAccessUser = DataAccessEntities.User1();
        userMapperMock.GetDataAccess(domainUser).Returns(dataAccessUser);

        await testee.AddUser(domainUser);

        var result = await dataContextMock.Users.ToListAsync();
        Assert.AreEqual(1, result.Count);
        Assert.IsTrue(compareLogic.Compare(dataAccessUser, result[0]).AreEqual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    [DataRow("")]
    [DataRow("  ")]
    [DataRow(null)]
    public async Task GetUserByEmail_WhenParameterIsNullEmptyOrWhitespace_ThrowException(string parameter)
    {
        await testee.GetUserByEmail(parameter);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task GetUserByEmail_WhenUserWasNotFound_ThrowException()
    {
        await testee.GetUserByEmail(Constants.EmailHash1);
    }

    [TestMethod]
    public async Task GetUserByEmail_ReturnsExpected()
    {
        var dataAccessUser = DataAccessEntities.User1();
        var domainUser = DomainEntities.HashedUser1();
        await dataContextMock.Users.AddAsync(dataAccessUser);
        dataContextMock.SaveChanges();
        userMapperMock.GetDomain(dataAccessUser).Returns(domainUser);

        var user = await testee.GetUserByEmail(Constants.EmailHash1);

        Assert.IsTrue(compareLogic.Compare(domainUser, user).AreEqual);
    }
}
