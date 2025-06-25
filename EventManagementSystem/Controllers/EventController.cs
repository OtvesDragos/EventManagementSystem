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
    public async Task<IActionResult> Edit([FromBody] Event eventToEdit)
    {
        var userId = User?.FindFirst("id")?.Value;

        if (userId == null)
        {
            return Unauthorized("User Id not found!");
        }

        await eventBusinessLogic.Edit(eventToEdit, Guid.Parse(userId));
        return Ok();
    }

    [Authorize]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] int ownerEmail)
    {
        var userId = User?.FindFirst("id")?.Value;

        if (userId == null)
        {
            return Unauthorized("User Id not found!");
        }

        await eventBusinessLogic.Delete(ownerEmail, Guid.Parse(userId));
        return Ok();
    }

    [Authorize]
    [HttpPost("GetAllByOwner")]
    public async Task<IActionResult> GetAllByOwner()
    {
        var userId = User?.FindFirst("id")?.Value;

        if (userId == null)
        {
            return Unauthorized("User Id not found!");
        }

        return Ok(await eventBusinessLogic.GetAllByOwner(Guid.Parse(userId)));
    }

}
