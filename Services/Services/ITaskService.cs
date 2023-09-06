using Services.Request.Board;

namespace Services.Services;

public interface ITaskService
{
    Task<Models.Board.Task?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Models.Board.Task>> GetAllAsync(Guid idSprint, CancellationToken cancellationToken);
    Task CreateAsync(TaskRequest task, CancellationToken cancellationToken);
    Task UpdateAsync(TaskUpdateRequest task, CancellationToken cancellationToken);
    Task AttachFilesAsync(Guid Id, List<FileRequest> files, CancellationToken cancellationToken);
}