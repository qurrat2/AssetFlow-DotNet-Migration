using System.Threading.Tasks;
using AssetFlow.Core.Entities;
using System.Collections.Generic;

namespace AssetFlow.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task<IReadOnlyList<User>> GetAllAsync();
}
