using StyleWardrobe.Api.Common.Exceptions;
using StyleWardrobe.Api.DTOs.Users;
using StyleWardrobe.Api.Models;
using StyleWardrobe.Api.Repositories;
using StyleWardrobe.Api.Services.CurrentUser;

namespace StyleWardrobe.Api.Services.Users;

public class UserService(
    IUserRepository userRepository,
    ICurrentUserService currentUserService) : IUserService
{
    public async Task<User> GetOrCreateCurrentUserEntityAsync()
    {
        var uid = currentUserService.UserId;
        if (string.IsNullOrWhiteSpace(uid))
        {
            throw new UnauthorizedException("User ID is missing from authorization token.");
        }

        var user = await userRepository.GetByFirebaseUidAsync(uid);
        if (user != null)
        {
            return user;
        }

        var email = currentUserService.Email ?? string.Empty;
        user = new User
        {
            FirebaseUid = uid,
            Email = email,
            DisplayName = email.Split('@').FirstOrDefault() ?? "User",
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        return user;
    }

    public async Task<UserResponse> GetMeAsync()
    {
        var user = await GetOrCreateCurrentUserEntityAsync();
        return new UserResponse(user.Id, user.Email, user.DisplayName, user.AvatarUrl);
    }

    public async Task<UserResponse> UpdateMeAsync(UpdateUserRequest request)
    {
        var user = await GetOrCreateCurrentUserEntityAsync();

        if (request.DisplayName != null)
        {
            user.DisplayName = request.DisplayName;
        }

        if (request.AvatarUrl != null)
        {
            user.AvatarUrl = request.AvatarUrl;
        }

        await userRepository.SaveChangesAsync();

        return new UserResponse(user.Id, user.Email, user.DisplayName, user.AvatarUrl);
    }
}
