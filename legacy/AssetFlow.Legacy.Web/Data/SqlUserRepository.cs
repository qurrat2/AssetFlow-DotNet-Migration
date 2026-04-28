using System.Data;
using System.Threading.Tasks;
using AssetFlow.Core.Entities;
using AssetFlow.Core.Repositories;
using Microsoft.Data.SqlClient;

namespace AssetFlow.Legacy.Web.Data;

public class SqlUserRepository : IUserRepository
{
    private readonly string _connStr;
    public SqlUserRepository(string connStr) => _connStr = connStr;

    public async Task<User> GetByUsernameAsync(string username)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("sp_GetUserByUsername", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@Username", username);
        await conn.OpenAsync();
        using var r = await cmd.ExecuteReaderAsync();
        return await r.ReadAsync()
            ? new User
            {
                Id = r.GetInt32(0),
                Username = r.GetString(1),
                PasswordHash = r.GetString(2),
                DepartmentId = r.GetInt32(3),
                Role = r.GetString(4),
                CreatedAt = r.GetDateTime(5)
            }
            : null;
    }
}