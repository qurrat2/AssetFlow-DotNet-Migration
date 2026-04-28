using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetFlow.Core.Contracts;
using AssetFlow.Core.Entities;
using AssetFlow.Core.Repositories;

namespace AssetFlow.Core.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _repo;

    public AssetService(IAssetRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public async Task<IReadOnlyList<AssetDto>> ListForUserAsync(User currentUser)
    {
        if (currentUser == null) throw new ArgumentNullException(nameof(currentUser));

        IEnumerable<Asset> source;
        if (string.Equals(currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            // Admin path: walk every department. Repository returns dept-scoped results,so we aggregate.
            var depts = new HashSet<int>();
           
            source = await GetAllAsAdminAsync();
        }
        else
        {
            source = await _repo.GetByDepartmentAsync(currentUser.DepartmentId);
        }

        return source.Select(ToDto).ToList();
    }

    private Task<IReadOnlyList<Asset>> GetAllAsAdminAsync() => _repo.GetAllAsync();

    private static AssetDto ToDto(Asset a) => new AssetDto
    {
        Id = a.Id,
        Tag = a.Tag,
        Description = a.Description,
        Status = a.Status,
        DepartmentId = a.DepartmentId,
        AssignedToUserId = a.AssignedToUserId
    };
}