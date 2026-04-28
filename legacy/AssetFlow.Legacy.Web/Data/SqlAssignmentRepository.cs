using System.Data;
using System.Threading.Tasks;
using AssetFlow.Core.Repositories;
using Microsoft.Data.SqlClient;

namespace AssetFlow.Legacy.Web.Data;

public class SqlAssignmentRepository : IAssignmentRepository
{
    private readonly string _connStr;
    public SqlAssignmentRepository(string connStr) => _connStr = connStr;

    public async Task AssignAsync(int assetId, int userId, string notes)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("sp_AssignAsset", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@AssetId", assetId);
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@Notes", (object)notes ?? System.DBNull.Value);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task ReturnAsync(int assignmentId)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("sp_ReturnAsset", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@AssignmentId", assignmentId);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
}