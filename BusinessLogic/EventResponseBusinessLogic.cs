using BusinessLogic.Contracts;
using Domain.Entities;
using Repository.Contracts;

namespace BusinessLogic;
public class EventResponseBusinessLogic : IEventResponseBusinessLogic
{
    private readonly IEventResponseRepository eventResponseRepository;

    public EventResponseBusinessLogic(IEventResponseRepository eventResponseRepository)
    {
        this.eventResponseRepository = eventResponseRepository;
    }

    public async Task RespondToEvent(EventResponse response, Guid userId = default)
    {
        if (response == null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        await eventResponseRepository.RespondToEvent(response, userId);
    }
}
