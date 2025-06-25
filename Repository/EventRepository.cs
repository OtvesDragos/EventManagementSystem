using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

    public async Task Delete(int code, Guid ownerId)
    {
        var eventToDelete = await dataContext.Events.FirstOrDefaultAsync(x => x.Code == code);

        if (eventToDelete == null)
        {
            throw new InvalidOperationException("The event was not found in the DB");
        }

        if (eventToDelete.CreatedBy != ownerId)
        {
            throw new UnauthorizedAccessException("You are not allowed to delete an event that you did not create!");
        }

        dataContext.Remove(eventToDelete);
        await dataContext.SaveChangesAsync();
    }

    public async Task Edit(Event @event, Guid ownerId)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var eventToEdit = await dataContext.Events.FirstOrDefaultAsync(x => x.Code == @event.Code);

        if (eventToEdit == null)
        {
            throw new InvalidOperationException("The event was not found in the DB");
        }

        if (eventToEdit.CreatedBy != ownerId)
        {
            throw new UnauthorizedAccessException("You are not allowed to edit an event that you did not create!");
        }

        eventToEdit.Location = @event.Location;
        eventToEdit.Timestamp = @event.Timestamp;
        eventToEdit.Description = @event.Description;
        eventToEdit.Name = @event.Name;

        await dataContext.SaveChangesAsync();
    }

    public async Task<IList<Event>> GetAllByOwner(Guid ownerId)
    {
        if (ownerId == default)
        {
            throw new ArgumentException(nameof(ownerId));
        }

        var events = await dataContext.Events
            .Where(x => x.CreatedBy == ownerId)
            .ToListAsync();

        return events.Select(eventMapper.GetDomain).ToList();
    }
}
