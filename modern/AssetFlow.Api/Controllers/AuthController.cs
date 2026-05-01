using AssetFlow.Api.Auth;
using AssetFlow.Core.Contracts;
using AssetFlow.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AssetFlow.Api.Controllers;

[ApiController, Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly IJwtTokenService _jwt;

    public AuthController(IUserRepository users, IJwtTokenService jwt)
    {
        _users = users; _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest req)
    {
        var user = await _users.GetByUsernameAsync(req.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            return Unauthorized();

        return new LoginResponse
        {
            Token = _jwt.Issue(user),
            Username = user.Username,
            Role = user.Role,
            DepartmentId = user.DepartmentId
        };
    }
}