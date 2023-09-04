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
    private readonly IProjectProvider _projectProvider;

    public UserService(IUserProvider userProvider, IRoleProvider roleProvider, ITokenProvider tokenProvider,
        ITaskProvider taskProvider, IProjectProvider projectProvider)
    {
        _userProvider = userProvider;
        _roleProvider = roleProvider;
        _tokenProvider = tokenProvider;
        _taskProvider = taskProvider;
        _projectProvider = projectProvider;
    }

    public async Task RegistrationAsync(RegistrationCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (await _userProvider.FindAsync(command.Login, cancellationToken).ConfigureAwait(false) is not null)
            throw new ExistIsEntityException("Такой логин уже занят");
        
        var role = await _roleProvider.FindAsync(command.Role, cancellationToken);
        if (role is null)
            throw new ExistIsEntityException("Такой роли нет");
        
        
        var user = new User(command.Login, command.Password, role, command.Phone);
        await _userProvider.CreateAsync(user, cancellationToken);
    }
    public async Task RecoverPasswordAsync(RecoverPasswordCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        var user = await _userProvider.FindAsync(command.Login, cancellationToken).ConfigureAwait(false);
        if (user is not null)
            throw new ExistIsEntityException("Такого пользователя не существует.");
        if(user.Phone != command.Phone)
            throw new ArgumentException("Введён неправильный номер телефона.");
        
        user.PasswordReset(command.Password);
        
        await _userProvider.UpdateAsync(user, cancellationToken);
    }

    public async Task<Token> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        var user = await _userProvider.FindAsync(command.Login, cancellationToken).ConfigureAwait(false);

        if (user is null || !user.Verify(command.Password))
            throw new NotExistException("Неверный логин или пароль");

        return _tokenProvider.CreateToken(user);
    }
    public async Task<User?> FindAsync(string login, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(login);

        return await _userProvider.FindAsync(login, cancellationToken).ConfigureAwait(false);
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
    public async Task AttachProjectAsync(string login, Guid projectId, CancellationToken cancellationToken)
    {
        var user = await _userProvider.FindAsync(login, cancellationToken).ConfigureAwait(false);
        if (user is null)
            throw new NotExistException("Такого пользователя не существует");
        
        var project = await _projectProvider.GetAsync(projectId, cancellationToken).ConfigureAwait(false);
        if (project is null)
            throw new NotExistException("Такой задачи не существует");
        if (user.Role.Name != "Пользователь")
            throw new RoleMismatchException("Данная роль не подходит для данного действия.");

        user.Project = project;

        await _userProvider.UpdateAsync(user, cancellationToken);
    }
}