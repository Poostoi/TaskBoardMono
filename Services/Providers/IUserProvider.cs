using Models.UserActivity;

namespace Services.Providers;

public interface IUserProvider
{
    Task<User?> FindAsync(string login, CancellationToken cancellationToken);
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}