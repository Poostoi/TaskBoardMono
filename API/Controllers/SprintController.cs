using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Board;
using Services.Request.Board;
using Services.Services;
using TaskBoard.Authorization.API.Controllers;

namespace API.Controllers;

public class SprintController : ApiBaseController
{
    private readonly ISprintService _sprintService;
    private readonly IUserService _userService;


    public SprintController(ISprintService sprintService, IUserService userService)
    {
        _sprintService = sprintService;
        _userService = userService;
    }

    [Authorize(Roles = "Администратор, Менеджер")]
    [HttpPost(Name = "CreateSprint")]
    public async Task<IActionResult> CreateAsync(SprintRequest sprint)
    {
        ArgumentNullException.ThrowIfNull(sprint);
        await _sprintService.CreateAsync(sprint, new CancellationToken());
        return Ok();
    }

    [Authorize(Roles = "Администратор, Менеджер")]
    [HttpPut(Name = "UpdateSprint")]
    public async Task<IActionResult> UpdateAsync(SprintRequest sprint)
    {
        ArgumentNullException.ThrowIfNull(sprint);
        await _sprintService.UpdateAsync(sprint, new CancellationToken());
        return Ok();
    }

    [Authorize]
    [HttpGet("GetAllProduct")]
    public async Task<List<Sprint>> GetAll(string login, Guid idProject)
    {
        var user = await _userService.FindAsync(login, new CancellationToken());
        if (user.Role.Name == "Пользователь" && user.Project.Id != idProject)
            throw new ArgumentException("Пользователь не добавлен в проект.");
        var listProject = await _sprintService.GetAllAsync(idProject, new CancellationToken());
        return listProject;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<Sprint> GetById(Guid id, string login, Guid idProject)
    {
        var user = await _userService.FindAsync(login, new CancellationToken());
        if (user.Role.Name == "Пользователь" && user.Project.Id != idProject)
            throw new ArgumentException("Пользователь не добавлен в проект.");
        var sprint = await _sprintService.GetAsync(id, new CancellationToken());
        return sprint;
    }
}