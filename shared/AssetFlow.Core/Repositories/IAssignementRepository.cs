using System.Threading.Tasks;

namespace AssetFlow.Core.Repositories;

public interface IAssignmentRepository
{
    Task AssignAsync(int assetId, int userId, string notes);
    Task ReturnAsync(int assignmentId);
}