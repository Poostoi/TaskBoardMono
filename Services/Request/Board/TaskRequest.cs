using Microsoft.AspNetCore.Http;

namespace Services.Request.Board;

public class TaskRequest
{
    public Guid SprintId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
    public List<IFormFile> Files { get; set; }
}