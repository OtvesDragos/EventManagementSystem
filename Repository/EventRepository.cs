using DataAccess;
using Domain.Entities;
using Repository.Contracts;
using Repository.Contracts.Mappers;

namespace Repository;
public class EventRepository : IEventRepository
{
    private readonly IMapper<Event, DataAccess.Entities.Event> eventMapper;
    private readonly DataContext dataContext;

    public EventRepository(IMapper<Event, DataAccess.Entities.Event> eventMapper, DataContext dataContext)
    {
        this.eventMapper = eventMapper;
        this.dataContext = dataContext;
    }

    public async Task Create(Event @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        await dataContext.Events.AddAsync(eventMapper.GetDataAccess(@event));
        await dataContext.SaveChangesAsync();
    }

    public Task Delete(int code)
    {
        throw new NotImplementedException();
    }

    public Task Edit(Event @event)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Event>> GetAllByOwner(string ownerEmail)
    {
        throw new NotImplementedException();
    }
}
