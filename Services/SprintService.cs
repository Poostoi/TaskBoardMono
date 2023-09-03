using Models.Board;
using Services.Exception;
using Services.Providers;
using Services.Request.Board;
using Services.Services;
using File = Models.Board.File;
using Task = System.Threading.Tasks.Task;

namespace Services;

public class SprintService : ISprintService
{
    public SprintService(ISprintProvider sprintProvider, IProjectProvider projectProvider)
    {
        _sprintProvider = sprintProvider;
        _projectProvider = projectProvider;
    }

    private readonly ISprintProvider _sprintProvider;
    private readonly IProjectProvider _projectProvider;

    public async Task<Sprint?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var sprint = await _sprintProvider.GetAsync(id, cancellationToken);
        if (sprint == null)
            throw new NotExistException("Такого спринта не существует");
        return sprint;
    }

    public async Task CreateAsync(SprintRequest sprint, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(sprint);

        if (await _sprintProvider.FindAsync(sprint.Name, cancellationToken).ConfigureAwait(false) != null)
            throw new ExistIsEntityException("Такое название спринта существует");

        var projectDb = await _projectProvider.GetAsync(sprint.ProjectId, cancellationToken).ConfigureAwait(false);
        if (projectDb == null)
            throw new NotExistException("Такого проекта не существует");

        if (sprint.DateStart > sprint.DateEnd)
            throw new DateTimeException("Время начала проекта не может быть больше времени конца проекта.");

        if (sprint.DateStart != sprint.DateEnd)
            throw new DateTimeException("Время начала проекта не может быть равным времени конца проекта.");
        
        var sprintDb = new Sprint(projectDb, sprint.Name, sprint.Description, sprint.DateStart, sprint.DateEnd,
            sprint.Comment, new List<File>());
        
        sprintDb.Files.AddRange(ConvertImageInArrayByte(sprint.Files, sprintDb));
        await _sprintProvider.CreateAsync(sprintDb, cancellationToken);
    }


    public async Task<List<Sprint>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _sprintProvider.GetAllAsync(cancellationToken);
    }

    public async Task UpdateAsync(SprintRequest sprint, CancellationToken cancellationToken)
    {
        var sprintDb = await _sprintProvider.FindAsync(sprint.Name, cancellationToken);
        if (sprintDb == null)
            throw new NotExistException("Такого сприната не существует");
        var projectDb = await _projectProvider.GetAsync(sprint.ProjectId, cancellationToken).ConfigureAwait(false);
        if (projectDb == null)
            throw new NotExistException("Такого проекта не существует");

        if (sprint.DateStart > sprint.DateEnd)
            throw new DateTimeException("Время начала проекта не может быть больше времени конца проекта.");

        if (sprint.DateStart != sprint.DateEnd)
            throw new DateTimeException("Время начала проекта не может быть равным времени конца проекта.");
        sprintDb.Project = projectDb;
        sprintDb.Name = sprint.Name;
        sprintDb.Description = sprint.Description;
        sprintDb.DateStart = sprint.DateStart;
        sprintDb.DateEnd = sprint.DateEnd;
        sprintDb.DateUpdate = DateTime.Now;
        sprintDb.Files.AddRange(ConvertImageInArrayByte(sprint.Files, sprintDb));
        await _sprintProvider.UpdateAsync(sprintDb, cancellationToken);
    }


    private List<File> ConvertImageInArrayByte(List<FileRequest> filesRequests,
        Sprint sprint)
    {
        var files = new List<File>();
        foreach (var file in filesRequests)
        {
            var fileCurrent = new File(file.Name, null, sprint,null);
            using (var binaryReader = new BinaryReader(file.DataImage.OpenReadStream()))
            {
                fileCurrent.DataImage = binaryReader.ReadBytes((int)file.DataImage.Length);
            }

            files.Add(fileCurrent);
        }

        return files;
    }
}