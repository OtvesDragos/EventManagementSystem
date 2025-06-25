
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

    public static Event Event1()
    {
        return new Event
        {
            Name = Constants.Name1,
            Description = Constants.Description1,
            CreatedBy = Constants.CreatedBy1,
            Location = Constants.Location1,
            Timestamp = Constants.Timestamp1
        };
    }

    public static Event Event2()
    {
        return new Event
        {
            Name = Constants.Name1,
            Description = Constants.Description1,
            CreatedBy = Constants.CreatedBy1,
            Location = Constants.Location1,
            Timestamp = Constants.Timestamp1
        };
    }

    public static IList<Event> Events()
    {
        return new List<Event>
        {
            Event1(),
            Event2()
        };
    }
}
