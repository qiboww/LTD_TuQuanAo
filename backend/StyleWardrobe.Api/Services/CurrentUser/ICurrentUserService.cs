namespace StyleWardrobe.Api.Services.CurrentUser;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
}
