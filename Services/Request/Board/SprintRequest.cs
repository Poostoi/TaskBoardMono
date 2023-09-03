namespace Services.Request;

public class SprintRequest
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Comment { get; set; }
    public List<FileRequest> Files { get; set; }
}