using Models.Board;
using Services.Request.Board;
using Task = System.Threading.Tasks.Task;

namespace Services.Services;

public interface IProjectService
{
    Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(ProjectRequest project, CancellationToken cancellationToken);
    Task UpdateAsync(ProjectRequest project, CancellationToken cancellationToken);
}