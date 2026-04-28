using AssetFlow.Core.Entities;
using AssetFlow.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;


namespace AssetFlow.Legacy.Web.Data
{
    public class SqlAssetRepository : IAssetRepository
    {
        private readonly String _connStr;
        public SqlAssetRepository(String connStr) => _connStr = connStr;

        public async Task<IReadOnlyList<Asset>> GetByDepartmentAsync(int departmentId)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_GetAssetsByDepartment", conn) { CommandType = System.Data.CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var results = new List<Asset>();
            while (await reader.ReadAsync())
                results.Add(MapAsset(reader));
            return results;
        }
        public async Task<Asset> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_GetAssetById", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapAsset(reader) : null;
        }

        public async Task<IReadOnlyList<Asset>> GetAllAsync()
        {
            // No "all" sproc to keep the demo small. Use ad-hoc SQL — also a valid pattern.
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(
                "SELECT Id, Tag, Description, Status, DepartmentId, AssignedToUserId, CreatedAt FROM dbo.Assets ORDER BY Tag",
                conn);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var results = new List<Asset>();
            while (await reader.ReadAsync())
                results.Add(MapAsset(reader));
            return results;
        }

        private static Asset MapAsset(SqlDataReader r) => new Asset
        {
            Id = r.GetInt32(0),
            Tag = r.GetString(1),
            Description = r.GetString(2),
            Status = r.GetString(3),
            DepartmentId = r.GetInt32(4),
            AssignedToUserId = r.IsDBNull(5) ? (int?)null : r.GetInt32(5),
            CreatedAt = r.GetDateTime(6)
        };
    }
}