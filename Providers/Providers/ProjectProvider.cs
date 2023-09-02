using Microsoft.EntityFrameworkCore;
using Models.Board;
using Services.Providers;
using Task = System.Threading.Tasks.Task;

namespace Providers.Providers;

public class ProjectProvider: IProjectProvider
{
    private readonly ApplicationContext _applicationContext;

    public ProjectProvider(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var project = await _applicationContext.Projects
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken: cancellationToken).ConfigureAwait(false);
        return project;
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _applicationContext.Projects
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task CreateAsync(Project project, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(project);
        _applicationContext.Add(project);
        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(Project project, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(project);
        _applicationContext.Update(project);
        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}