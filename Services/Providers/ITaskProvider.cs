namespace Services.Providers;

public interface ITaskProvider
{
    Task<Models.Board.Task?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Models.Board.Task?> FindAsync(string name, CancellationToken cancellationToken);
    Task<List<Models.Board.Task>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Models.Board.Task task, CancellationToken cancellationToken);
    Task UpdateAsync(Models.Board.Task task, CancellationToken cancellationToken);
}