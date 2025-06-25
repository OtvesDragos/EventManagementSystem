using Domain.Entities;

namespace Repository.Contracts;
public interface IEventRepository
{
    Task Create(Event @event);
    Task Edit(Event @event);
    Task Delete(int code);
    Task<IList<Event>> GetAllByOwner(string ownerEmail);
}
