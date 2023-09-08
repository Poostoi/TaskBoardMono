using Microsoft.AspNetCore.Http;
using Models.Board;
using Services.Exception;
using Services.Providers;
using Services.Request.Board;
using Services.Response;
using Services.Services;
using Task = System.Threading.Tasks.Task;

namespace Services;

public class TaskService: ITaskService
{
    public TaskService(ITaskProvider taskProvider, ISprintProvider sprintProvider)
    {
        _taskProvider = taskProvider;
        _sprintProvider = sprintProvider;
    }

    private readonly ITaskProvider _taskProvider;
    private readonly ISprintProvider _sprintProvider;

    public async Task<TaskResponse?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskProvider.GetAsync(id, cancellationToken);
        if (task == null)
            throw new NotExistException("Такой задачи не существует");
        return ConvertInTaskResponse(task);
    }

    public async Task CreateAsync(TaskRequest task, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(task);

        if (await _taskProvider.FindAsync(task.Name, cancellationToken).ConfigureAwait(false) != null)
            throw new ExistIsEntityException("Такое название задачи существует");

        var sprintDb = await _sprintProvider.GetAsync(task.SprintId, cancellationToken).ConfigureAwait(false);
        if (sprintDb == null)
            throw new NotExistException("Такого спринта не существует");

        
        var taskDb = new Models.Board.Task(sprintDb,
            task.Name,
            task.Description,
            (StatusEnum) Enum.Parse(typeof(StatusEnum),
                task.Status, true),
            task.Comment,
            new List<Models.Board.File>());
        
        taskDb.Files.AddRange(ConvertFileInArrayByte(task.Files, taskDb));
        await _taskProvider.CreateAsync(taskDb, cancellationToken);
    }


    public async Task<List<TaskResponse>> GetAllAsync(Guid idSprint, CancellationToken cancellationToken)
    {
        var sprintDb = await _sprintProvider.GetAsync(idSprint, cancellationToken).ConfigureAwait(false);
        if (sprintDb == null)
            throw new NotExistException("Такого спринта не существует");
        var taskDbList = await _taskProvider.GetAllAsync(idSprint, cancellationToken);
        var taskResponseList = new List<TaskResponse>();
        foreach (var taskDb in taskDbList)
        {
            taskResponseList.Add(ConvertInTaskResponse(taskDb));
        }
        return taskResponseList;
    }

    public async Task UpdateAsync(TaskUpdateRequest task, CancellationToken cancellationToken)
    {
        var taskDb = await _taskProvider.GetAsync(task.Id, cancellationToken);
        if (taskDb == null)
            throw new NotExistException("Такой задачи не существует");
        var sprintDb = await _sprintProvider.GetAsync(task.SprintId, cancellationToken).ConfigureAwait(false);
        if (sprintDb == null)
            throw new NotExistException("Такого спринта не существует");
        
        taskDb.Sprint = sprintDb;
        taskDb.Name = task.Name;
        taskDb.Description = task.Description;
        taskDb.Status = (StatusEnum)Enum.Parse(typeof(StatusEnum),
            task.Status, true);
        taskDb.DateUpdate = DateTime.Now;
        taskDb.Comment = task.Comment;
        taskDb.Files.AddRange(ConvertFileInArrayByte(task.Files, taskDb));
        await _taskProvider.UpdateAsync(taskDb, cancellationToken);
    }
    public async Task AttachFilesAsync(Guid id,  List<IFormFile> files, CancellationToken cancellationToken)
    {
        var taskDb = await _taskProvider.GetAsync(id, cancellationToken);
        if (taskDb == null)
            throw new NotExistException("Такой задачи не существует");
        
        taskDb.DateUpdate = DateTime.Now;
        taskDb.Files.AddRange(ConvertFileInArrayByte(files, taskDb));
        await _taskProvider.UpdateAsync(taskDb, cancellationToken);
    }


    private List<Models.Board.File> ConvertFileInArrayByte(List<IFormFile> filesRequests,
        Models.Board.Task task)
    {
        var files = new List<Models.Board.File>();
        foreach (var fileRequest in filesRequests)
        {
            var fileCurrent = new Models.Board.File(fileRequest.Name, null, null,task);
            using (var binaryReader = new BinaryReader(fileRequest.OpenReadStream()))
            {
                fileCurrent.DataImage = binaryReader.ReadBytes((int)fileRequest.Length);
            }

            files.Add(fileCurrent);
        }

        return files;
    }

    private TaskResponse ConvertInTaskResponse(Models.Board.Task task)
    {
        return new TaskResponse()
        {
            SprintId = task.Sprint.Id,
            Name = task.Name,
            Description = task.Description,
            Status = task.Status,
            Comment = task.Comment,
            Files = ConvertInFileResponses(task.Files)
        };
    }

    private List<FileResponse> ConvertInFileResponses(List<Models.Board.File> files)
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