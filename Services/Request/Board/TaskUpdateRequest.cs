namespace Services.Request.Board;

public class TaskUpdateRequest
{
    public Guid Id { get; set; }
    public Guid SprintId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
    public List<FileRequest> Files { get; set; }
}