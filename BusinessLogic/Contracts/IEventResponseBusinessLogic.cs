using Domain.Entities;

namespace BusinessLogic.Contracts;
public interface IEventResponseBusinessLogic
{
    Task RespondToEvent(EventResponse response, Guid userId);
}
