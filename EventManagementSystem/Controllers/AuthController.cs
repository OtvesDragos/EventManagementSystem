using BusinessLogic.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthBusinessLogic authBusinessLogic;

    public AuthController(IAuthBusinessLogic authBusinessLogic)
    {
        this.authBusinessLogic = authBusinessLogic;
    }

    [HttpPost("Authenticate")]
    public async Task Authenticate([FromBody] User user)
    {
        await authBusinessLogic.Authenticate(user);
    }
}
