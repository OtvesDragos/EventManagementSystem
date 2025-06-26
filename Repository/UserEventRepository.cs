using DataAccess;
using DataAccess.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;
public class UserEventRepository : IUserEventRepository
{
    private readonly DataContext dataContext;

    public UserEventRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task AddInvites(IList<Guid> userIds, Guid eventId, Guid ownerId)
    {
        if (userIds == null)
        {
            throw new ArgumentNullException(nameof(userIds));
        }

        var @event = await dataContext.Events
            .Where(e => e.Id == eventId)
            .FirstOrDefaultAsync();

        if (@event == null)
        {
            throw new InvalidOperationException($"Event with code {eventId} not found.");
        }

        if (@event.CreatedBy != ownerId)
        {
            throw new UnauthorizedAccessException("You are not allowed to add invites to this event!");
        }

        var userEvents = userIds.Select(x => new UserEvent
        {
            EventId = eventId,
            UserId = x
        });

        await dataContext.AddRangeAsync(userEvents);
        await dataContext.SaveChangesAsync();
    }
}
