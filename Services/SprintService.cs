using Microsoft.AspNetCore.Http;
using Models.Board;
using Services.Exception;
using Services.Providers;
using Services.Request.Board;
using Services.Response;
using Services.Services;
using File = Models.Board.File;
using Task = System.Threading.Tasks.Task;

namespace Services;

public class SprintService : ISprintService
{
    public SprintService(ISprintProvider sprintProvider, IProjectProvider projectProvider, IFileProvider fileProvider)
    {
        _sprintProvider = sprintProvider;
        _projectProvider = projectProvider;
        _fileProvider = fileProvider;
    }

    private readonly ISprintProvider _sprintProvider;
    private readonly IProjectProvider _projectProvider;
    private readonly IFileProvider _fileProvider;

    public async Task<SprintResponse?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var sprint = await _sprintProvider.GetAsync(id, cancellationToken);
        if (sprint == null)
            throw new NotExistException("Такого спринта не существует");
        return ConvertInSprintResponse(sprint);
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
            throw new DateTimeException("Время начала спринта не может быть больше времени конца спринта.");

        if (sprint.DateStart == sprint.DateEnd)
            throw new DateTimeException("Время начала спринта не может быть равным времени конца спринта.");

        var sprintDb = new Sprint(projectDb, sprint.Name, sprint.Description, sprint.DateStart, sprint.DateEnd,
            sprint.Comment, new List<File>());

        sprintDb.Files.AddRange(ConvertFileInArrayByte(sprint.Files, sprintDb));
        await _sprintProvider.CreateAsync(sprintDb, cancellationToken);
    }


    public async Task<List<SprintResponse>> GetAllAsync(Guid idProject, CancellationToken cancellationToken)
    {
        if (await _projectProvider.GetAsync(idProject, cancellationToken).ConfigureAwait(false) == null)
            throw new NotExistException("Такого проекта не существует");
        var sprintDbList = await _sprintProvider.GetAllAsync(idProject, cancellationToken);
        var sprintResponseList = new List<SprintResponse>();
        foreach (var sprintDb in sprintDbList)
        {
            sprintResponseList.Add(ConvertInSprintResponse(sprintDb));
        }
        return sprintResponseList;
    }

    public async Task UpdateAsync(SprintUpdateRequest sprint, CancellationToken cancellationToken)
    {
        var sprintDb = await _sprintProvider.GetAsync(sprint.Id, cancellationToken);
        if (sprintDb == null)
            throw new NotExistException("Такого спринта не существует");
        var projectDb = await _projectProvider.GetAsync(sprint.ProjectId, cancellationToken).ConfigureAwait(false);
        if (projectDb == null)
            throw new NotExistException("Такого проекта не существует");

        if (sprint.DateStart > sprint.DateEnd)
            throw new DateTimeException("Время начала спринта не может быть больше времени конца спринта.");

        if (sprint.DateStart == sprint.DateEnd)
            throw new DateTimeException("Время начала спринта не может быть равным времени конца спринта.");
        sprintDb.Project = projectDb;
        sprintDb.Name = sprint.Name;
        sprintDb.Description = sprint.Description;
        sprintDb.DateStart = sprint.DateStart;
        sprintDb.DateEnd = sprint.DateEnd;
        sprintDb.DateUpdate = DateTime.Now;
        sprintDb.Files.AddRange(ConvertFileInArrayByte(sprint.Files, sprintDb));
        await _sprintProvider.UpdateAsync(sprintDb, cancellationToken);
    }

    public async Task AttachFilesAsync(Guid id, List<IFormFile> files, CancellationToken cancellationToken)
    {
        var sprintDb = await _sprintProvider.GetAsync(id, cancellationToken);
        if (sprintDb == null)
            throw new NotExistException("Такого спринта не существует");

        sprintDb.DateUpdate = DateTime.Now;
        var filesDb = ConvertFileInArrayByte(files, sprintDb);
        await _sprintProvider.UpdateAsync(sprintDb, cancellationToken);
        await _fileProvider.CreateAsync(filesDb, cancellationToken);
    }


    private List<File> ConvertFileInArrayByte(List<IFormFile> filesRequests,
        Sprint sprint)
    {
        var files = new List<File>();
        foreach (var fileRequest in filesRequests)
        {
            var fileCurrent = new File(fileRequest.FileName, null, sprint, null);
            using (var binaryReader = new BinaryReader(fileRequest.OpenReadStream()))
            {
                fileCurrent.DataImage = binaryReader.ReadBytes((int)fileRequest.Length);
            }

            files.Add(fileCurrent);
        }

        return files;
    }

    private SprintResponse ConvertInSprintResponse(Sprint sprint) => new SprintResponse()
    {
        ProjectId = sprint.Project.Id,
        Name = sprint.Name,
        Description = sprint.Description,
        DateStart = sprint.DateStart,
        DateEnd = sprint.DateEnd,
        Comment = sprint.Comment,
        Files = ConvertInFileResponses(sprint.Files)
    };

    private List<FileResponse> ConvertInFileResponses(List<File> files)
    {
        var filesResponse = new List<FileResponse>();
        foreach (var file in files)
        {
            var fileCurrent = new FileResponse()
            {
                Name = file.Name,
                DataImage = file.DataImage,
                SprintId = null,
                TaskId = null
            };
            if (file.Task != null)
                fileCurrent.TaskId = file.Task.Id;
            if (file.Sprint != null)
                fileCurrent.SprintId = file.Sprint.Id;

            filesResponse.Add(fileCurrent);
        }

        return filesResponse;
    }
}