namespace Services.Request;

public class TaskRequest
{
    public Sprint Sprint { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StatusEnum Status { get; set; }
    public string Comment { get; set; }
    public List<File> Files { get; set; }
}