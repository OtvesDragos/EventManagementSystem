using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Services.Constants;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services;
public class JwtTokenService : IJwtTokenService
{
    public string GetToken(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return ComposeJwtToken(user);
    }

    private string ComposeJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString())
        };


        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(File.ReadAllText("encryption_key.txt"))); // encryption_key.txt should be stored in a secure location
        var tokenCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtToken = new JwtSecurityToken(
            issuer: Keys.Server,
            audience: Keys.Server,
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: tokenCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
