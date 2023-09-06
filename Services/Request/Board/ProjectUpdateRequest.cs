namespace Services.Request.Board;

public class ProjectUpdateRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}