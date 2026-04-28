using System.Collections.Generic;
using System.Threading.Tasks;
using AssetFlow.Core.Contracts;
using AssetFlow.Core.Entities;

namespace AssetFlow.Core.Services;

public interface IAssetService
{
    Task<IReadOnlyList<AssetDto>> ListForUserAsync(User currentUser);
    
}