using Domain.Entities;

namespace Repository.Contracts;
public interface IEventRepository
{
    Task Create(Event @event);
    Task Edit(Event @event, Guid ownerId);
    Task Delete(int code, Guid ownerId);
    Task<IList<Event>> GetAllByOwner(Guid ownerId);
}
