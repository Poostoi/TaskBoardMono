using Models.UserActivity;
using Services.Providers;
using Services.Request.User;
using Services.Services;
using Services.Exception;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserProvider _userProvider;
    private readonly IRoleProvider _roleProvider;
    private readonly ITokenProvider _tokenProvider;
    private readonly ITaskProvider _taskProvider;

    public UserService(IUserProvider userProvider, IRoleProvider roleProvider, ITokenProvider tokenProvider,
        ITaskProvider taskProvider)
    {
        _userProvider = userProvider;
        _roleProvider = roleProvider;
        _tokenProvider = tokenProvider;
        _taskProvider = taskProvider;
    }

    public async Task RegistrationAsync(RegistrationCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (await _userProvider.FindAsync(command.Login, cancellationToken).ConfigureAwait(false) is not null)
            throw new ExistIsEntityException("Такой логин уже занят");

        var role = await _roleProvider.FindAsync(command.Role, cancellationToken);

        var user = new User(command.Login, command.Password, role);
        await _userProvider.CreateAsync(user, cancellationToken);
    }

    public async Task<Token> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        var user = await _userProvider.FindAsync(command.Login, cancellationToken).ConfigureAwait(false);

        if (user is null || !user.Verify(command.Password))
            throw new NotExistException("Неверный логин или пароль");

        return _tokenProvider.CreateToken(user);
    }

    public async Task AttachTaskAsync(string login, Guid taskId, CancellationToken cancellationToken)
    {
        var user = await _userProvider.FindAsync(login, cancellationToken).ConfigureAwait(false);
        if (user is null)
            throw new NotExistException("Такого пользователя не существует");

        var task = await _taskProvider.GetAsync(taskId, cancellationToken).ConfigureAwait(false);
        if (task is null)
            throw new NotExistException("Такой задачи не существует");

        user.Task = task;

        await _userProvider.UpdateAsync(user, cancellationToken);
    }
}