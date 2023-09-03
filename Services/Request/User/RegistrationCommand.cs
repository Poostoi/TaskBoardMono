namespace Services.Request.User;

public class RegistrationCommand
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}