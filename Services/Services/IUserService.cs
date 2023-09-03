using Services.Request.User;

namespace Services.Services;

public interface IUserService
{
    Task RegistrationAsync(RegistrationCommand command, CancellationToken cancellationToken);
    Task<Token> LoginAsync(LoginCommand command, CancellationToken cancellationToken);
}