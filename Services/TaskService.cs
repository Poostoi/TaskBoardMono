using Models.Board;
using Services.Exception;
using Services.Providers;
using Services.Request.Board;
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

    public async Task<Models.Board.Task?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskProvider.GetAsync(id, cancellationToken);
        if (task == null)
            throw new NotExistException("Такой задачи не существует");
        return task;
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
        
        taskDb.Files.AddRange(ConvertImageInArrayByte(task.Files, taskDb));
        await _taskProvider.CreateAsync(taskDb, cancellationToken);
    }


    public async Task<List<Models.Board.Task>> GetAllAsync(Guid idSprint, CancellationToken cancellationToken)
    {
        var sprintDb = await _sprintProvider.GetAsync(idSprint, cancellationToken).ConfigureAwait(false);
        if (sprintDb == null)
            throw new NotExistException("Такого спринта не существует");
        return await _taskProvider.GetAllAsync(idSprint,cancellationToken);
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
        taskDb.Files.AddRange(ConvertImageInArrayByte(task.Files, taskDb));
        await _taskProvider.UpdateAsync(taskDb, cancellationToken);
    }
    public async Task AttachFilesAsync(Guid id,  List<FileRequest> files, CancellationToken cancellationToken)
    {
        var taskDb = await _taskProvider.GetAsync(id, cancellationToken);
        if (taskDb == null)
            throw new NotExistException("Такой задачи не существует");
        
        taskDb.DateUpdate = DateTime.Now;
        taskDb.Files.AddRange(ConvertImageInArrayByte(files, taskDb));
        await _taskProvider.UpdateAsync(taskDb, cancellationToken);
    }


    private List<Models.Board.File> ConvertImageInArrayByte(List<FileRequest> filesRequests,
        Models.Board.Task task)
    {
        var files = new List<Models.Board.File>();
        foreach (var file in filesRequests)
        {
            var fileCurrent = new Models.Board.File(file.Name, null, null,task);
            using (var binaryReader = new BinaryReader(file.DataImage.OpenReadStream()))
            {
                fileCurrent.DataImage = binaryReader.ReadBytes((int)file.DataImage.Length);
            }

            files.Add(fileCurrent);
        }

        return files;
    }
}