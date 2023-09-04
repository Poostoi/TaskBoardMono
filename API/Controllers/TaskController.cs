using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Request.Board;
using Services.Services;
using TaskBoard.Authorization.API.Controllers;

namespace API.Controllers;

public class TaskController: ApiBaseController
{
    private readonly ITaskService _taskService;
    private readonly IUserService _userService;


    public TaskController(ITaskService taskService, IUserService userService)
    {
        _taskService = taskService;
        _userService = userService;
    }

    [Authorize(Roles = "Администратор, Менеджер")]
    [HttpPost(Name = "CreateTask")]
    public async Task<IActionResult> CreateAsync(TaskRequest task)
    {
        ArgumentNullException.ThrowIfNull(task);
        await _taskService.CreateAsync(task, new CancellationToken());
        return Ok();
    }

    [Authorize(Roles = "Администратор, Менеджер")]
    [HttpPut(Name = "UpdateTask")]
    public async Task<IActionResult> UpdateAsync(TaskRequest task)
    {
        ArgumentNullException.ThrowIfNull(task);
        await _taskService.UpdateAsync(task, new CancellationToken());
        return Ok();
    }

    [Authorize]
    [HttpGet(Name = "GetAllProduct")]
    public async Task<List<Models.Board.Task>> GetAllAsync(string login, Guid idProject, Guid idSprint)
    {
        var user = await _userService.FindAsync(login, new CancellationToken());
        if (user.Role.Name == "Пользователь" && user.Project.Id != idProject)
            throw new ArgumentException("Пользователь не добавлен в проект.");
        var listProject = await _taskService.GetAllAsync(idSprint, new CancellationToken());
        return listProject;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<Models.Board.Task> GetByIdAsync(Guid id, string login, Guid idProject)
    {
        var user = await _userService.FindAsync(login, new CancellationToken());
        if (user.Role.Name == "Пользователь" && user.Project.Id != idProject)
            throw new ArgumentException("Пользователь не добавлен в проект.");
        var task = await _taskService.GetAsync(id, new CancellationToken());
        return task;
    }
}