using AssetFlow.Core.Entities;
using AssetFlow.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AssetFlow.Api.Data;

public class EfAssignmentRepository : IAssignmentRepository
{
    private readonly AppDbContext _db;
    public EfAssignmentRepository(AppDbContext db) => _db = db;

    public async Task AssignAsync(int assetId, int userId, string notes)
    {
        var asset = await _db.Assets.FirstOrDefaultAsync(a => a.Id == assetId)
                  ?? throw new InvalidOperationException("Asset not found");
        if (asset.Status != "Available")
            throw new InvalidOperationException("Asset not available");
        _db.Assignments.Add(new Assignment
        {
            AssetId = assetId,
            UserId = userId,
            Notes = notes,
            AssignedOn = DateTime.UtcNow
        });
        asset.Status = "Assigned";
        asset.AssignedToUserId = userId;
        await _db.SaveChangesAsync();
    }

    public async Task ReturnAsync(int assignmentId)
    {
        var a = await _db.Assignments.FirstOrDefaultAsync(x => x.Id == assignmentId)
              ?? throw new InvalidOperationException("Assignment not found");
        if (a.ReturnedOn != null) throw new InvalidOperationException("Already returned");
        a.ReturnedOn = DateTime.UtcNow;
        var asset = await _db.Assets.FirstAsync(x => x.Id == a.AssetId);
        asset.Status = "Available";
        asset.AssignedToUserId = null;
        await _db.SaveChangesAsync();
    }
}