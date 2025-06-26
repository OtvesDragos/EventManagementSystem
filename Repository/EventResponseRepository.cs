using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.Contracts.Mappers;
using Services.Constants;
using Services.Contracts;
using System.Threading.Tasks;

namespace Repository;
public class EventResponseRepository : IEventResponseRepository
{
    private readonly DataContext dataContext;
    private readonly IEventRepository eventRepository;
    private readonly IMapper<EventResponse, DataAccess.Entities.EventResponse> evenetResponseMapper;
    private readonly IHashService hashService;


    public EventResponseRepository(DataContext dataContext, IEventRepository eventRepository,
        IMapper<EventResponse, DataAccess.Entities.EventResponse> evenetResponseMapper,
        IHashService hashService)
    {
        this.dataContext = dataContext;
        this.eventRepository = eventRepository;
        this.evenetResponseMapper = evenetResponseMapper;
        this.hashService = hashService;
    }

    public async Task RespondToEvent(EventResponse response, Guid userId = default)
    {
        if (response == null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        var @event = await eventRepository.GetByCode(response.EventCode);

        if (!(await CheckIfAuthorized(@event, userId)))
        {
            throw new UnauthorizedAccessException("You cannot respond to this event!");
        }

        if (!(await EditResponse(response)))
        {
            await dataContext.EventResponses.AddAsync(evenetResponseMapper.GetDataAccess(response));
            await dataContext.SaveChangesAsync();
        }
    }

    private async Task<bool> EditResponse(EventResponse response)
    {
        var emailHash = hashService.GetSha256Hash(response.Email);

        var dataAccessResponse = await dataContext.EventResponses
            .FirstOrDefaultAsync(x => x.EventCode == response.EventCode && x.Email == emailHash);

        if (dataAccessResponse == null)
        {
            return false;
        }

        dataAccessResponse.Name = response.Name;
        dataAccessResponse.Response = response.Response;

        await dataContext.SaveChangesAsync();
        return true;
    }

    private async Task<bool> CheckIfAuthorized(Event @event, Guid userId)
    {
       return string.Equals(@event.Visibility, Keys.Public, StringComparison.InvariantCultureIgnoreCase)
            || dataContext.UserEvents.Any(x => x.UserId == userId && x.EventId == @event.Id);
    }
}
