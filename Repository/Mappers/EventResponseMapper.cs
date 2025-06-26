using Domain.Entities;
using Repository.Contracts.Mappers;
using Services.Contracts;

namespace Repository.Mappers;
public class EventResponseMapper : IMapper<EventResponse, DataAccess.Entities.EventResponse>
{
    private readonly IHashService hashService;

    public EventResponseMapper(IHashService hashService)
    {
        this.hashService = hashService;
    }

    public DataAccess.Entities.EventResponse GetDataAccess(EventResponse user)
    {
        return new DataAccess.Entities.EventResponse
        {
            Id = Guid.NewGuid(),
            Name = user.Name.Trim(),
            Email = hashService.GetSha256Hash(user.Email.Trim()),
            EventCode = user.EventCode,
            Response = user.Response.ToLower().Trim(),
        };
    }

    public EventResponse GetDomain(DataAccess.Entities.EventResponse user)
    {
        return new EventResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            EventCode = user.EventCode,
            Response = user.Response,
        };
    }
}
