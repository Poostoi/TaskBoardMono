using Microsoft.AspNetCore.Http;
using Services.Request.Board;
using Services.Response;

namespace Services.Services;

public interface ITaskService
{
    Task<TaskResponse?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<TaskResponse>> GetAllAsync(Guid idSprint, CancellationToken cancellationToken);
    Task CreateAsync(TaskRequest task, CancellationToken cancellationToken);
    Task UpdateAsync(TaskUpdateRequest task, CancellationToken cancellationToken);
    Task AttachFilesAsync(Guid Id, List<IFormFile> files, CancellationToken cancellationToken);
}