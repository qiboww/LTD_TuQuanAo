using Microsoft.EntityFrameworkCore;
using StyleWardrobe.Api.Data;
using StyleWardrobe.Api.Models;

namespace StyleWardrobe.Api.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id)
    {
        return await dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetByFirebaseUidAsync(string firebaseUid)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.FirebaseUid == firebaseUid);
    }

    public async Task AddAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
