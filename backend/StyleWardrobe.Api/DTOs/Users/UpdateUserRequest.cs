using System.ComponentModel.DataAnnotations;

namespace StyleWardrobe.Api.DTOs.Users;

public class UpdateUserRequest
{
    [StringLength(100, ErrorMessage = "displayName must not exceed 100 characters")]
    public string? DisplayName { get; set; }

    [Url(ErrorMessage = "avatarUrl must be a valid URL")]
    [StringLength(500, ErrorMessage = "avatarUrl must not exceed 500 characters")]
    public string? AvatarUrl { get; set; }
}
