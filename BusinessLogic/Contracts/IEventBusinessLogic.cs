using Domain.Entities;

namespace BusinessLogic.Contracts;
public interface IEventBusinessLogic
{
    Task Create(Event @event);
    Task Edit (Event @event, Guid ownerId);
    Task Delete (int code, Guid ownerId);
    Task<IList<Event>> GetAllByOwner(Guid ownerId);
    Task<IList<Event>> GetAllPublic();
    Task<Event> GetByCode(int eventCode);
}
