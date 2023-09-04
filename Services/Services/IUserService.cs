using Models.UserActivity;
using Services.Request.User;

namespace Services.Services;

public interface IUserService
{
    Task RegistrationAsync(RegistrationCommand command, CancellationToken cancellationToken);
    Task<Token> LoginAsync(LoginCommand command, CancellationToken cancellationToken);
    Task<User?> FindAsync(string login, CancellationToken cancellationToken);
    Task RecoverPasswordAsync(RecoverPasswordCommand command, CancellationToken cancellationToken);
}