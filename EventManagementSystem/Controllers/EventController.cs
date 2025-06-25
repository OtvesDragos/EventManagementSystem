using BusinessLogic.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventBusinessLogic eventBusinessLogic;

    public EventController(IEventBusinessLogic eventBusinessLogic)
    {
        this.eventBusinessLogic = eventBusinessLogic;
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task Create([FromBody] Event eventToCreate)
    {
        await eventBusinessLogic.Create(eventToCreate);
    }

    [Authorize]
    [HttpPut("Edit")]
    public async Task Edit([FromBody] Event eventToEdit)
    {
        await eventBusinessLogic.Edit(eventToEdit);
    }

    [Authorize]
    [HttpDelete("Delete")]
    public async Task Delete([FromBody] int ownerEmail)
    {
        await eventBusinessLogic.Delete(ownerEmail);
    }

    [Authorize]
    [HttpPost("GetAllByOwner")]
    public async Task<IList<Event>> GetAllByOwner(string ownerEmail)
    {
        return await eventBusinessLogic.GetAllByOwner(ownerEmail);
    }

}
