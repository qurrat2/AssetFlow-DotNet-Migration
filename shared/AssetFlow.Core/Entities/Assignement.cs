namespace AssetFlow.Core.Entities;

public class Assignment
{
    public int Id { get; set; }
    public int AssetId { get; set; }
    public int UserId { get; set; }
    public System.DateTime AssignedOn { get; set; }
    public System.DateTime? ReturnedOn { get; set; }
    public string Notes { get; set; }
}