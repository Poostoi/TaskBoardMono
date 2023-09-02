using Models.UserActivity;

namespace Services.Providers;

public interface IRoleProvider
{
    Task<Role?> FindAsync(string name, CancellationToken cancellationToken);
    Task CreateAsync(Role role, CancellationToken cancellationToken);
}