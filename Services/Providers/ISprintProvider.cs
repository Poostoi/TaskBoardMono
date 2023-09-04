using Models.Board;
using Task = System.Threading.Tasks.Task;

namespace Services.Providers;

public interface ISprintProvider
{
    Task<Sprint> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Sprint?> FindAsync(string name, CancellationToken cancellationToken);
    Task<List<Sprint>> GetAllAsync(Guid idProject, CancellationToken cancellationToken);
    Task CreateAsync(Sprint sprint, CancellationToken cancellationToken);
    Task UpdateAsync(Sprint sprint, CancellationToken cancellationToken);
}