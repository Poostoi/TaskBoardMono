namespace Services.Response;

public class FileResponse
{
    public string Name { get; set; }
    public byte[]? DataImage { get; set; }
    public Guid? SprintId { get; set; }
    public Guid? TaskId { get; set; }
}