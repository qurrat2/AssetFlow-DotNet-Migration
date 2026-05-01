using AssetFlow.Core.Entities;

namespace AssetFlow.Api.Auth;

public interface IJwtTokenService
{
    string Issue(User user);
}