using Microsoft.EntityFrameworkCore;
using Models.Board;
using Services.Providers;
using Task = System.Threading.Tasks.Task;

namespace Providers.Providers;

public class SprintProvider: ISprintProvider
{
    private readonly ApplicationContext _applicationContext;

    public SprintProvider(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task<Sprint?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var sprint = await _applicationContext.Sprints
            .Include(s => s.Project)
            .Include(s => s.Files)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken: cancellationToken).ConfigureAwait(false);
        return sprint;
    }

    public async Task<List<Sprint>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _applicationContext.Sprints
            .Include(s => s.Project)
            .Include(s => s.Files)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task CreateAsync(Sprint sprint, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(sprint);
        _applicationContext.Add(sprint);
        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(Sprint sprint, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(sprint);
        _applicationContext.Update(sprint);
        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}