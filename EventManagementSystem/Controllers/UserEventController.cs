using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

namespace EventManagementSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEventController : ControllerBase
{
    private readonly IUserEventBusinessLogic userEventBusinessLogic;

    public UserEventController(IUserEventBusinessLogic userEventBusinessLogic)
    {
        this.userEventBusinessLogic = userEventBusinessLogic;   
    }

    [Authorize]
    [HttpPost("AddInvites")]
    public async Task<IActionResult> AddInvites(Guid eventId, IList<Guid> userIds)
    {
        var ownerId = User?.FindFirst("id")?.Value;

        if (ownerId == null)
        {
            return Unauthorized("User Id not found!");
        }

        await userEventBusinessLogic.AddInvites(userIds, eventId, Guid.Parse(ownerId));

        return Ok();
    }
}
