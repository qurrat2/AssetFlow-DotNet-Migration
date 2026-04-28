using System.Collections.Generic;
using System.Threading.Tasks;
using AssetFlow.Core.Entities;

namespace AssetFlow.Core.Repositories;

public interface IAssetRepository
{
    Task<IReadOnlyList<Asset>> GetByDepartmentAsync(int departmentId);
    Task<Asset> GetByIdAsync(int id);
    Task<IReadOnlyList<Asset>> GetAllAsync();
}