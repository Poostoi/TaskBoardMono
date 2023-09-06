using Models.Board;
using Services.Exception;
using Services.Providers;
using Services.Request.Board;
using Services.Services;
using Task = System.Threading.Tasks.Task;

namespace Services;

public class ProjectService : IProjectService
{
    private readonly IProjectProvider _projectProvider;

    public ProjectService(IProjectProvider projectProvider)
    {
        _projectProvider = projectProvider;
    }

    public async Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projectProvider.GetAsync(id, cancellationToken);
        if (project == null)
            throw new NotExistException("Такого проекта не существует");
        return project;
    }

    public async Task CreateAsync(ProjectRequest project, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(project);
        
        if (await _projectProvider.FindAsync(project.Name, cancellationToken).ConfigureAwait(false) != null)
            throw new ExistIsEntityException("Такое название проекта существует");
        var projectDb = new Project(project.Name, project.Description);
        await _projectProvider.CreateAsync(projectDb, cancellationToken);
    }


    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _projectProvider.GetAllAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProjectUpdateRequest project, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(project);
        var projectDb = await _projectProvider.GetAsync(project.Id, cancellationToken);
        if (projectDb == null)
            throw new NotExistException("Такого проекта не существует");
        projectDb.Name = project.Name;
        projectDb.Description = project.Description;
        projectDb.DateUpdate = DateTime.Now;
        await _projectProvider.UpdateAsync(projectDb, cancellationToken);
    }
}