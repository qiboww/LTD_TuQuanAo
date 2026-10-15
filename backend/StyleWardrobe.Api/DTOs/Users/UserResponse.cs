namespace StyleWardrobe.Api.DTOs.Users;

public record UserResponse(
    int Id,
    string Email,
    string? DisplayName,
    string? AvatarUrl
);
