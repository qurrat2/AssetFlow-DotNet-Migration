#nullable disable
using AssetFlow.Core.Entities;
using AssetFlow.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AssetFlow.Api.Data;

public class EfUserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public EfUserRepository(AppDbContext db) => _db = db;

    public async Task<User> GetByUsernameAsync(string username) =>
        await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);

    public async Task<IReadOnlyList<User>> GetAllAsync() =>
      await _db.Users.AsNoTracking().OrderBy(u => u.Username).ToListAsync();

}