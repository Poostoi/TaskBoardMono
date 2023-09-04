namespace Services.Request.User;

public class RecoverPasswordCommand
{
    public string Login { get; set; }
    
    public string Phone { get; set; }
    public string Password { get; set; }
}