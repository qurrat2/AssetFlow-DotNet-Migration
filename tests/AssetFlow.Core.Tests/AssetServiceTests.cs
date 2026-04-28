using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetFlow.Core.Entities;
using AssetFlow.Core.Repositories;
using AssetFlow.Core.Services;
using Xunit;

namespace AssetFlow.Core.Tests;

public class AssetServiceTests
{
    private static User Staff(int id, int dept) =>
        new User { Id = id, Username = "s", DepartmentId = dept, Role = "Staff" };
    private static User Admin(int id) =>
        new User { Id = id, Username = "a", DepartmentId = 0, Role = "Admin" };

    private class FakeAssetRepo : IAssetRepository
    {
        public List<Asset> All { get; } = new();
        public Task<IReadOnlyList<Asset>> GetByDepartmentAsync(int dept) =>
            Task.FromResult<IReadOnlyList<Asset>>(All.Where(a => a.DepartmentId == dept).ToList());
        public Task<Asset> GetByIdAsync(int id) =>
            Task.FromResult(All.FirstOrDefault(a => a.Id == id));
        public Task<IReadOnlyList<Asset>> GetAllAsync() =>
            Task.FromResult<IReadOnlyList<Asset>>(All);
    }

    [Fact]
    public async Task Staff_user_sees_only_own_department_assets()
    {
        var repo = new FakeAssetRepo();
        repo.All.Add(new Asset { Id = 1, Tag = "A", DepartmentId = 1, Status = "Available" });
        repo.All.Add(new Asset { Id = 2, Tag = "B", DepartmentId = 2, Status = "Available" });
        var svc = new AssetService(repo);

        var result = await svc.ListForUserAsync(Staff(10, 1));

        Assert.Single(result);
        Assert.Equal("A", result[0].Tag);
    }

    [Fact]
    public async Task Admin_user_sees_all_assets_via_admin_path()
    {
        var repo = new FakeAssetRepo();
        repo.All.Add(new Asset { Id = 1, Tag = "A", DepartmentId = 1, Status = "Available" });
        repo.All.Add(new Asset { Id = 2, Tag = "B", DepartmentId = 2, Status = "Available" });
        var svc = new AssetService(repo);

        var result = await svc.ListForUserAsync(Admin(99));

        Assert.Equal(2, result.Count);
    }
}