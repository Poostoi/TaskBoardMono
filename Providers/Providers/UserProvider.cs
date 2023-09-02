using Microsoft.EntityFrameworkCore;
using Models.UserActivity;
using Services.Providers;

namespace Providers.Providers;

public class UserProvider: IUserProvider
{
    private readonly ApplicationContext _applicationContext;

    public UserProvider(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<User?> FindAsync(string login, CancellationToken cancellationToken)
    {
        var user = await _applicationContext.Users
            .Include(u => u.Role)
            .Include(u => u.Sprint)
            .Include(u => u.Task)
            .FirstOrDefaultAsync(d => d.Login == login, cancellationToken: cancellationToken).ConfigureAwait(false);
        return user;

    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        _applicationContext.Add(user);
        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _applicationContext.Update(user);
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}