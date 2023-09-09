using Microsoft.AspNetCore.Http;

namespace Services.Response;

public class SprintResponse
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Comment { get; set; }
    public List<FileResponse> Files { get; set; }
}