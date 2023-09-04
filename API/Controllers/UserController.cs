using Microsoft.AspNetCore.Mvc;
using Services.Request.User;
using Services.Services;
using TaskBoard.Authorization.API.Controllers;

namespace API.Controllers;

public class UserController : ApiBaseController
{
    private readonly IUserService _userService;


    public UserController(IUserService userService)
    {
        ArgumentNullException.ThrowIfNull(userService);

        _userService = userService;
    }

    [HttpPost("Registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await _userService.RegistrationAsync(command, cancellationToken).ConfigureAwait(false);
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var token = await _userService.LoginAsync(command, cancellationToken).ConfigureAwait(false);
        return Ok(token);
    }
    [HttpPost("RecoverPassword")]
    public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await _userService.RecoverPasswordAsync(command, cancellationToken).ConfigureAwait(false);
        return Ok();
    }

    
}