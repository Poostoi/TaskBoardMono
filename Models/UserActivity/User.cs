using Models.Board;
using Task = Models.Board.Task;

namespace Models.UserActivity;

public class User: BaseEntity
{
    public User(string login, string password, Role? role, string phone)
    {
        ArgumentNullException.ThrowIfNull(login);
        ArgumentNullException.ThrowIfNull(password);
        Login = login;
        Salt = BCrypt.Net.BCrypt.GenerateSalt();
        PasswordHash = BCrypt.Net.BCrypt.HashPassword((Salt + password));
        Role = role;
        Phone = phone;
    }

    protected User(string phone)
    {
        Phone = phone;
    }

    public string Login { get; set; }
    public string Phone { get; set; }
    public string Salt { get; set; }
    public string PasswordHash { get; set; }
    public Task? Task { get; set; }
    public Role? Role { get; set; }
    public Project? Project { get; set; }
    public void PasswordReset(string password)
    {
        Salt = BCrypt.Net.BCrypt.GenerateSalt();
        PasswordHash = BCrypt.Net.BCrypt.HashPassword((Salt + password));
    }
    public bool Verify(string password)
    {
        return BCrypt.Net.BCrypt.Verify((Salt + password), PasswordHash);
    }
}