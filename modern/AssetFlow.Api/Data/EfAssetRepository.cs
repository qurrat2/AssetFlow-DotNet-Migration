using AssetFlow.Core.Entities;
using AssetFlow.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AssetFlow.Api.Data;

public class EfAssetRepository : IAssetRepository
{
    private readonly AppDbContext _db;
    public EfAssetRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<Asset>> GetByDepartmentAsync(int departmentId) =>
        await _db.Assets.AsNoTracking()
                  .Where(a => a.DepartmentId == departmentId)
                  .OrderBy(a => a.Tag).ToListAsync();

    public async Task<Asset?> GetByIdAsync(int id) =>
        await _db.Assets.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

    public async Task<IReadOnlyList<Asset>> GetAllAsync() =>
        await _db.Assets.AsNoTracking().OrderBy(a => a.Tag).ToListAsync();
}