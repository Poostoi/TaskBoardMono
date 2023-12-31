﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Board;
using Services.Request.Board;
using Services.Response;
using Services.Services;
using TaskBoard.Authorization.API.Controllers;
using File = Models.Board.File;

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
    public async Task<IActionResult> UpdateAsync(SprintUpdateRequest sprint)
    {
        ArgumentNullException.ThrowIfNull(sprint);
        await _sprintService.UpdateAsync(sprint, new CancellationToken());
        return Ok();
    }

    [Authorize]
    [HttpGet("GetAllSprint")]
    public async Task<List<SprintResponse>> GetAll(string login, Guid idProject)
    {
        var user = await _userService.FindAsync(login, new CancellationToken());
        if (user.Role.Name == "Пользователь" && user.Project.Id != idProject)
            throw new ArgumentException("Пользователь не добавлен в проект.");
        
        return await _sprintService.GetAllAsync(idProject, new CancellationToken());
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<SprintResponse> GetById(Guid id, string login, Guid idProject)
    {
        var user = await _userService.FindAsync(login, new CancellationToken());
        if (user.Role.Name == "Пользователь" && user.Project.Id != idProject)
            throw new ArgumentException("Пользователь не добавлен в проект.");
        var sprint = await _sprintService.GetAsync(id, new CancellationToken());
        return sprint;
    }
    [Authorize(Roles = "Администратор, Менеджер")]
    [HttpPut("{id}")]
    public async Task<IActionResult> AttachFiles(Guid id,  List<IFormFile> files, CancellationToken cancellationToken)
    {
        if (files.Count == 0)
            throw new ArgumentException("Не прикреплено ни одного фаила.");
        
        await _sprintService.AttachFilesAsync(id,files, cancellationToken).ConfigureAwait(false);
        return Ok();
    }
    
}