using StyleWardrobe.Api.DTOs.Users;
using StyleWardrobe.Api.Models;

namespace StyleWardrobe.Api.Services.Users;

public interface IUserService
{
    Task<User> GetOrCreateCurrentUserEntityAsync();
    Task<UserResponse> GetMeAsync();
    Task<UserResponse> UpdateMeAsync(UpdateUserRequest request);
}
