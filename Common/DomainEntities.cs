
using Domain.Entities;

namespace Common;
public static class DomainEntities
{
    public static User User1()
    {
        return new User
        {
            Email = Constants.Email1,
            Password = Constants.Password1,
            FirstName = Constants.FirstName1,
            LastName = Constants.LastName1,
        };
    }

    public static User HashedUser1()
    {
        return new User
        {
            Email = Constants.EmailHash1,
            Password = Constants.PasswordHash1,
            FirstName = Constants.FirstName1,
            LastName = Constants.LastName1,
        };
    }

    public static Credentials Credentials1()
    {
        return new Credentials
        {
            Email = Constants.Email1,
            Password = Constants.Password1
        };
    }
}
