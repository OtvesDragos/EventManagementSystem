using BusinessLogic.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventManagementSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class EventResponseController : ControllerBase
{
    private readonly IEventResponseBusinessLogic eventResponseBusinessLogic;

    public EventResponseController(IEventResponseBusinessLogic eventResponseBusinessLogic)
    {
        this.eventResponseBusinessLogic = eventResponseBusinessLogic;
    }

    [Authorize]
    [HttpPost("RespondToEvent/Authorized")]
    public async Task<IActionResult> RespondToEventAuthorized([FromBody] EventResponse response)
    {
        var userIdClaim = User?.FindFirst("id")?.Value;

        Guid userId = userIdClaim == null ? default : Guid.Parse(userIdClaim); 

        await eventResponseBusinessLogic.RespondToEvent(response, userId);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("RespondToEvent")]
    public async Task<IActionResult> RespondToEventAnonymous([FromBody] EventResponse response)
    {
        await eventResponseBusinessLogic.RespondToEvent(response, default);

        return Ok();
    }
}
