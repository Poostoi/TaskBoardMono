using Microsoft.EntityFrameworkCore;
using Services.Providers;

namespace Providers.Providers;

public class TaskProvider: ITaskProvider
{
    private readonly ApplicationContext _applicationContext;

    public TaskProvider(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task<Models.Board.Task?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _applicationContext.Tasks
            .Include(t => t.Sprint)
            .Include(t => t.Files)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken: cancellationToken).ConfigureAwait(false);
        return task;
    }

    public async Task<List<Models.Board.Task>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _applicationContext.Tasks
            .Include(t => t.Sprint)
            .Include(t => t.Files)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task CreateAsync(Models.Board.Task task, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(task);
        _applicationContext.Add(task);
        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(Models.Board.Task task, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(task);
        _applicationContext.Update(task);
        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}