using Common;
using Services;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;

namespace ServiceUnitTests;
[TestClass]
public sealed class JwtTokenServiceTests
{
    private IJwtTokenService testee;

    [TestInitialize]
    public void Setup()
    {
        testee = new JwtTokenService();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetToken_Throws_WhenUserIsNull()
    {
        testee.GetToken(null);
    }

    [TestMethod]
    public void GetToken_ReturnsValidJwt_WithUserIdClaim()
    {
        var user = DomainEntities.User1();
        user.Id = Constants.Id1;

        var token = testee.GetToken(user);

        var handler = new JwtSecurityTokenHandler();
        Assert.IsTrue(handler.CanReadToken(token), "Token cannot be read by handler.");

        var jwt = handler.ReadJwtToken(token);
        var idClaim = jwt.Claims.FirstOrDefault(c => c.Type == "id");
        Assert.IsNotNull(idClaim);
        Assert.AreEqual(Constants.Id1.ToString(), idClaim.Value);
    }
}
