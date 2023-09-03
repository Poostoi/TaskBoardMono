using Microsoft.EntityFrameworkCore;
using Models.UserActivity;
using Services.Providers;

namespace Providers.Providers;

public class RoleProvider: IRoleProvider
{
    private readonly ApplicationContext _applicationContext;

    public RoleProvider(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task CreateAsync(Role role, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role);
        _applicationContext.Add(role);
        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<Role?> FindAsync(string roleName, CancellationToken cancellationToken)
    {
        var role = await _applicationContext.Roles
            .FirstOrDefaultAsync(d => d.Name == roleName, cancellationToken: cancellationToken).ConfigureAwait(false);
        return role;
    }
   
}