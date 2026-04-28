namespace AssetFlow.Core.Entities;

public class Asset
{
    public int Id { get; set; }
    public string Tag { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }            // Available | Assigned | Retired
    public int DepartmentId { get; set; }
    public int? AssignedToUserId { get; set; }
    public System.DateTime CreatedAt { get; set; }
}