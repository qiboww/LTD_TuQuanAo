using System.Security.Claims;

namespace StyleWardrobe.Api.Services.CurrentUser;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public string? UserId =>
        User?.FindFirstValue("sub") ??
        User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? Email =>
        User?.FindFirstValue("email") ??
        User?.FindFirstValue(ClaimTypes.Email);

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
}
