using Domain.Entities;

namespace Repository.Contracts;
public interface IEventResponseRepository
{
    Task RespondToEvent(EventResponse response, Guid userId);
}
