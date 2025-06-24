using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using System.Text;

namespace Services;
public static class HashService
{
    public static string GetPasswordHash(string password)
    {
        return Argon2.Hash(password);
    }

    public static bool VerifyPassword(string password, string passwordHash)
    {
        return Argon2.Verify(passwordHash, password);
    }

    public static string GetSha256Hash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            var builder = new StringBuilder();
            foreach (var b in hashBytes)
                builder.Append(b.ToString("x2"));

            return builder.ToString();
        }
    }

    public static bool VerifySha256Hash(string input, string hash)
    {
        var actualHash = GetSha256Hash(input);

        return string.Equals(actualHash, hash, StringComparison.InvariantCultureIgnoreCase);
    }
}