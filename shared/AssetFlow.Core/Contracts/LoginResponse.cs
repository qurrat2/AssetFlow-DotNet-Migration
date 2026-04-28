namespace AssetFlow.Core.Contracts;

public class LoginResponse
{
    public string Token { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public int DepartmentId { get; set; }
}