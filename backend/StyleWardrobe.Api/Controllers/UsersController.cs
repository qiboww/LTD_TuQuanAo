using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StyleWardrobe.Api.DTOs.Users;
using StyleWardrobe.Api.Services.Users;

namespace StyleWardrobe.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<UserResponse>> GetMe()
    {
        var response = await userService.GetMeAsync();
        return Ok(response);
    }

    [HttpPut("me")]
    public async Task<ActionResult<UserResponse>> UpdateMe([FromBody] UpdateUserRequest request)
    {
        var response = await userService.UpdateMeAsync(request);
        return Ok(response);
    }
}
