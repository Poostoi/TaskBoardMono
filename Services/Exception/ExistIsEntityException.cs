namespace Cornelius.Product.Services.Exception;

public class ExistIsEntityException:System.Exception
{
    public ExistIsEntityException(string? message) : base(message)
    {
    }
}
