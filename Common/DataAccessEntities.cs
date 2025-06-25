using DataAccess.Entities;

namespace Common;
public static class DataAccessEntities
{
    public static User User1()
    {
        return new User
        {
            EmailHash = Constants.EmailHash1,
            PasswordHash = Constants.PasswordHash1,
            LastName = Constants.LastName1,
            FirstName = Constants.FirstName1,
        };
    }
}
