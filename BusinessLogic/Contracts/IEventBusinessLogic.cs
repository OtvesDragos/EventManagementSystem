using Domain.Entities;

namespace BusinessLogic.Contracts;
public interface IEventBusinessLogic
{
    Task Create(Event @event);
    Task Edit (Event @event);
    Task Delete (int code);
    Task<IList<Event>> GetAllByOwner(string ownerEmail);
}
