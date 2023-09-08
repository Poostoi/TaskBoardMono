using Microsoft.AspNetCore.Http;
using Models.Board;
using Services.Request.Board;
using Services.Response;
using Task = System.Threading.Tasks.Task;

namespace Services.Services;

public interface ISprintService
{
    Task<SprintResponse?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<SprintResponse>> GetAllAsync(Guid idProject, CancellationToken cancellationToken);
    Task CreateAsync(SprintRequest sprint, CancellationToken cancellationToken);
    Task UpdateAsync(SprintUpdateRequest sprint, CancellationToken cancellationToken);
    Task AttachFilesAsync(Guid Id, List<IFormFile> files, CancellationToken cancellationToken);
}