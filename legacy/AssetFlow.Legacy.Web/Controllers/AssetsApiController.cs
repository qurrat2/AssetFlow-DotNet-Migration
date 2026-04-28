using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AssetFlow.Core.Contracts;
using AssetFlow.Core.Entities;
using AssetFlow.Core.Services;
using AssetFlow.Legacy.Web.Auth;

namespace AssetFlow.Legacy.Web.Controllers;

[RoutePrefix("api/assets")]
public class AssetsApiController : ApiController
{
    private readonly IAssetService _svc;
    public AssetsApiController(IAssetService svc) => _svc = svc;

    [HttpGet, Route("")]
    public async Task<IHttpActionResult> List()
    {
        var current = FormsAuthHelper.Current();
        if (current == null) return Unauthorized();
        var user = new User
        {
            Id = current.Value.UserId,
            Role = current.Value.Role,
            DepartmentId = current.Value.DepartmentId
        };
        IReadOnlyList<AssetDto> result = await _svc.ListForUserAsync(user);
        return Ok(result);   // Newtonsoft serializes by default in Web API 2
    }
}