using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Board;
using Services.Request.Board;
using Services.Services;
using TaskBoard.Authorization.API.Controllers;

namespace API.Controllers;

public class ProjectController: ApiBaseController
{
    private readonly IProjectService _projectService;


    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    [Authorize(Roles = "Администратор,Менеджер")]
    [HttpPost(Name = "CreateProject")]
    public async Task<IActionResult> CreateAsync(ProjectRequest project)
    {
        ArgumentNullException.ThrowIfNull(project);
        await _projectService.CreateAsync(project, new CancellationToken());
        return Ok();
    }
    [Authorize(Roles = "Администратор, Менеджер")]
    [HttpPut(Name = "UpdateProject")]
    public async Task<IActionResult> UpdateAsync(ProjectUpdateRequest project)
    {
        ArgumentNullException.ThrowIfNull(project);
        await _projectService.UpdateAsync(project, new CancellationToken());
        return Ok();
    }
    [Authorize]
    [HttpGet(Name = "GetAllProject")]
    public async Task<List<Project>> GetAll()
    {
        var listProject = await _projectService.GetAllAsync(new CancellationToken());
        return listProject;
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<Project> GetById(Guid id)
    {
        var project = await _projectService.GetAsync(id, new CancellationToken());
        return project;
    }
}
