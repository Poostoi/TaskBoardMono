namespace Services.Exception;

public class DateTimeException:System.Exception
{
    public DateTimeException(string? message) : base(message)
    {
    }
}