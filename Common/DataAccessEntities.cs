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

    public static Event Event1()
    {
        return new Event
        {
            Id = Constants.Id1,
            Name = Constants.Name1,
            Description = Constants.Description1,
            CreatedBy = Constants.UserId1,
            Location = Constants.Location1,
            Timestamp = Constants.Timestamp1,
            Code = Constants.Code1,
        };
    }

    public static Event Event2()
    {
        return new Event
        {
            Id = Constants.Id2,
            Name = Constants.Name2,
            Description = Constants.Description2,
            CreatedBy = Constants.UserId1,
            Location = Constants.Location2,
            Timestamp = Constants.Timestamp2,
            Code = Constants.Code2,
        };
    }

    public static Event Event3()
    {
        return new Event
        {
            Id = Constants.Id3,
            Name = Constants.Name2,
            Description = Constants.Description3,
            CreatedBy = Constants.UserId2,
            Location = Constants.Location3,
            Timestamp = Constants.Timestamp3,
            Code = Constants.Code3,
        };
    }

    public static IList<Event> Events()
    {
        return new List<Event>
        {
            Event1(),
            Event2(),
            Event3()
        };
    }
}
