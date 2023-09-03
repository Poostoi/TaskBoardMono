﻿using Models.Board;
using Services.Request.Board;
using Task = System.Threading.Tasks.Task;

namespace Services.Services;

public interface ISprintService
{
    Task<Sprint?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Sprint>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(SprintRequest sprint, CancellationToken cancellationToken);
    Task UpdateAsync(SprintRequest sprint, CancellationToken cancellationToken);
}