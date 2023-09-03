using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models.UserActivity;
using Services.Providers;
using Services.Request;
using Services.Request.User;
using Services.Services;

namespace Providers.Providers;

public class TokenProvider: ITokenProvider
{
    private readonly IAuthOptions _authOptions;

    public TokenProvider(IAuthOptions authOptions)
    {
        _authOptions = authOptions;
    }

    public Token CreateToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_authOptions.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, Convert.ToString(user.Id)),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("Role", user.Role.Name)
            }),
            Expires = null,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token { Value = tokenHandler.WriteToken(token) };
    }
}