using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Services;

namespace DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<IProjectService, ProjectService>();
        service.AddScoped<ISprintService, SprintService>();
        service.AddScoped<ITaskService, TaskService>();
        return service;
    }
}