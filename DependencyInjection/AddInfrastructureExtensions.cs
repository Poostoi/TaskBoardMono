using Microsoft.Extensions.DependencyInjection;
using Providers;
using Providers.Providers;
using Services.Providers;

namespace DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service.AddDbContext<ApplicationContext>();
        service.AddCors(o => o.AddPolicy("ReactPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        service.AddScoped<IUserProvider, UserProvider>();
        service.AddScoped<IRoleProvider, RoleProvider>();
        service.AddScoped<IProjectProvider, ProjectProvider>();
        service.AddScoped<ISprintProvider, SprintProvider>();
        service.AddScoped<ITaskProvider, TaskProvider>();
        service.AddScoped<IFileProvider, FileProvider>();


        return service;
    }
}