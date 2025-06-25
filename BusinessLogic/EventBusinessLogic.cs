using BusinessLogic.Contracts;
using Domain.Entities;
using Repository.Contracts;

namespace BusinessLogic;
public class EventBusinessLogic : IEventBusinessLogic
{
    private readonly IEventRepository eventRepository;

    public EventBusinessLogic(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }

    public async Task Create(Event @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        await eventRepository.Create(@event);
    }

    public Task Delete(int code, Guid ownerId)
    {
        return eventRepository.Delete(code, ownerId);
    }

    public async Task Edit(Event @event, Guid ownerId)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        await eventRepository.Edit(@event, ownerId);
    }

    public async Task<IList<Event>> GetAllByOwner(Guid ownerId)
    {
        if (ownerId == default)
        {
            throw new ArgumentException(nameof(ownerId));
        }
        
        return await eventRepository.GetAllByOwner(ownerId);
    }
}
