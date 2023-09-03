using Services.Request.Board;

namespace Services.Services;

public interface ITaskService
{
    Task<Models.Board.Task?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Models.Board.Task>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(TaskRequest task, CancellationToken cancellationToken);
    Task UpdateAsync(TaskRequest task, CancellationToken cancellationToken);
}