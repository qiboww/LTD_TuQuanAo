using StyleWardrobe.Api.Models;

namespace StyleWardrobe.Api.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByFirebaseUidAsync(string firebaseUid);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}
