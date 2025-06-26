using DataAccess.Entities;
using Repository.Contracts.Mappers;

namespace Repository.Mappers;
public class EventMapper : IMapper<Domain.Entities.Event, Event>
{
    public Event GetDataAccess(Domain.Entities.Event @event)
    {
        var validVisibilities = new HashSet<string> { "public", "private" };

        if (@event.Visibility != null && !validVisibilities.Contains(@event.Visibility.ToLowerInvariant().Trim()))
        {
            throw new ArgumentException($"Invalid visibility: {@event.Visibility}");
        }
        return new Event
        {
            Id = Guid.NewGuid(),
            Name = @event.Name,
            Description = @event.Description,
            Timestamp = @event.Timestamp,
            Location = @event.Location,
            CreatedBy = @event.CreatedBy,
            Visibility = @event.Visibility,
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
            Visibility = @event.Visibility,
        };
    }
}
