namespace Services.Exception;

public class RoleMismatchException:System.Exception
{
    public RoleMismatchException(string? message) : base(message)
    {
    }
}