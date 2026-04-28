namespace AssetFlow.Core.Contracts;

public class AssignRequest
{
    public int AssetId { get; set; }
    public int UserId { get; set; }
    public string Notes { get; set; }
}