using System.Threading.Tasks;
using AssetFlow.Core.Entities;

namespace AssetFlow.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
}
