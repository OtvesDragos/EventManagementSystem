using DataAccess.Entities;
using Repository.Contracts.Mappers;

namespace Repository.Mappers;
public class EventMapper : IMapper<Domain.Entities.Event, Event>
{
    public Event GetDataAccess(Domain.Entities.Event user)
    {
        return new Event
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Description = user.Description,
            Timestamp = user.Timestamp,
            Location = user.Location,
            CreatedBy = user.CreatedBy,
        };
    }

    public Domain.Entities.Event GetDomain(Event user)
    {
        throw new NotImplementedException();
    }
}
