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

    public Task Delete(int code)
    {
        throw new NotImplementedException();
    }

    public async Task Edit(Event @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        await eventRepository.Edit(@event);
    }

    public Task<IList<Event>> GetAllByOwner(string ownerEmail)
    {
        throw new NotImplementedException();
    }
}
