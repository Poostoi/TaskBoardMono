using Models.Board;
using Services.Request.Board;
using Task = System.Threading.Tasks.Task;

namespace Services.Services;

public interface ISprintService
{
    Task<Sprint?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Sprint>> GetAllAsync(Guid idProject, CancellationToken cancellationToken);
    Task CreateAsync(SprintRequest sprint, CancellationToken cancellationToken);
    Task UpdateAsync(SprintUpdateRequest sprint, CancellationToken cancellationToken);
    Task AttachFilesAsync(Guid Id, List<FileRequest> files, CancellationToken cancellationToken);
}