namespace AssetFlow.Core.Contracts;

public class AssetDto
{
    public int Id { get; set; }
    public string Tag { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public int DepartmentId { get; set; }
    public int? AssignedToUserId { get; set; }
}
