using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Providers.Options;
using Providers.Providers;
using Services.Providers;

namespace DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorization(this IServiceCollection service, IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);
        var authOptions = config
            .GetSection(nameof(AuthOptions))
            .Get<AuthOptions>(options => options.BindNonPublicProperties = true);
        service.AddSingleton(authOptions);
        service.AddSingleton<ITokenProvider>(t => new TokenProvider(authOptions));
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = authOptions.ValidateIssuer,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = authOptions.ValidateAudience,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = authOptions.ValidateLifetime,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)),
                    ValidateIssuerSigningKey = authOptions.ValidateIssuerSigningKey,
                    RequireExpirationTime = authOptions.RequireExpirationTime
                };
            });
        service.AddAuthorization(); 
        return service;
    }

}