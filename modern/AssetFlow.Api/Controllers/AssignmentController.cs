using AssetFlow.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetFlow.Api.Controllers;

[ApiController, Authorize(Roles = "Admin"), Route("api/assignments")]
public class AssignmentsController : ControllerBase
{
    private readonly IAssignmentRepository _repo;
    public AssignmentsController(IAssignmentRepository repo) => _repo = repo;

    public record AssignBody(int AssetId, int UserId, string? Notes);

    [HttpPost]
    public async Task<IActionResult> Assign(AssignBody body)
    {
        try { await _repo.AssignAsync(body.AssetId, body.UserId, body.Notes ?? ""); return NoContent(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{id:int}/return")]
    public async Task<IActionResult> Return(int id)
    {
        try { await _repo.ReturnAsync(id); return NoContent(); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }
}