using Models.Board;
using Task = System.Threading.Tasks.Task;

namespace Services.Providers;

public interface IProjectProvider
{
    Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Project?> FindAsync(string name, CancellationToken cancellationToken);
    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Project project, CancellationToken cancellationToken);
    Task UpdateAsync(Project project, CancellationToken cancellationToken);
}