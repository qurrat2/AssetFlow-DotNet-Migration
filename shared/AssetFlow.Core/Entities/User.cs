namespace AssetFlow.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public int DepartmentId { get; set; }
    public string Role { get; set; }
    public System.DateTime CreatedAt { get; set; }
}