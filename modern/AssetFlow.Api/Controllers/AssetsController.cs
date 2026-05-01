using AssetFlow.Core.Entities;
using AssetFlow.Core.Repositories;
using AssetFlow.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssetFlow.Api.Controllers;

[ApiController, Authorize, Route("api/assets")]
public class AssetsController : ControllerBase
{
    private readonly IAssetService _svc;
    private readonly IAssetRepository _repo;
    public AssetsController(IAssetService svc, IAssetRepository repo)
    {
        _svc = svc; _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var u = CurrentUser();
        return Ok(await _svc.ListForUserAsync(u));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var asset = await _repo.GetByIdAsync(id);
        return asset == null ? NotFound() : Ok(asset);
    }

    private User CurrentUser() => new User
    {
        Id = int.Parse(User.FindFirst("sub")!.Value),
        Role = User.FindFirstValue(ClaimTypes.Role) ?? "Staff",
        DepartmentId = int.Parse(User.FindFirst("departmentId")!.Value)
    };
}