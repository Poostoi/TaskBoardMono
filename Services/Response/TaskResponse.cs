using Models.Board;

namespace Services.Response;

public class TaskResponse
{
    
    public Guid Id { get; set; }
    public Guid SprintId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StatusEnum Status { get; set; }
    public string Comment { get; set; }
    public List<FileResponse> Files { get; set; }
}