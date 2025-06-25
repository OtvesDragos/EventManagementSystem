using DataAccess.Entities;
using Repository.Contracts.Mappers;

namespace Repository.Mappers;
public class EventMapper : IMapper<Domain.Entities.Event, Event>
{
    public Event GetDataAccess(Domain.Entities.Event @event)
    {
        return new Event
        {
            Id = Guid.NewGuid(),
            Name = @event.Name,
            Description = @event.Description,
            Timestamp = @event.Timestamp,
            Location = @event.Location,
            CreatedBy = @event.CreatedBy,
        };
    }

    public Domain.Entities.Event GetDomain(Event @event)
    {
        return new Domain.Entities.Event
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            Timestamp = @event.Timestamp,
            Location = @event.Location,
            CreatedBy = @event.CreatedBy,
            Code = @event.Code,
        };
    }
}
