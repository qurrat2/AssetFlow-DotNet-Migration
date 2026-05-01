using System.Net;
using System.Net.Http.Json;
using AssetFlow.Core.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AssetFlow.Api.Tests;

public class AssetsEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public AssetsEndpointsTests(WebApplicationFactory<Program> f) => _factory = f;

    [Fact]
    public async Task Login_with_valid_credentials_returns_jwt()
    {
        var client = _factory.CreateClient();
        var resp = await client.PostAsJsonAsync("/api/auth/login",
            new LoginRequest { Username = "alice", Password = "Password123!" });

        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        var body = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(body);
        Assert.False(string.IsNullOrEmpty(body!.Token));
        Assert.Equal("alice", body.Username);
    }

    [Fact]
    public async Task Login_with_bad_password_returns_401()
    {
        var client = _factory.CreateClient();
        var resp = await client.PostAsJsonAsync("/api/auth/login",
            new LoginRequest { Username = "alice", Password = "wrong" });
        Assert.Equal(HttpStatusCode.Unauthorized, resp.StatusCode);
    }
}