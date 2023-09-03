using Models.UserActivity;
using Services.Request;
using Services.Request.User;

namespace Services.Providers;

public interface ITokenProvider
{
    Token CreateToken(User user);
}