
namespace Services.Contracts;
public interface IHashService
{
    string GetPasswordHash(string password);
    bool VerifyPassword(string password, string passwordHash);
    string GetSha256Hash(string input);
    bool VerifySha256Hash(string input, string hash);
}
