namespace Models.Board;

public class Task: BaseEntity
{
    public Task(Sprint sprint, string name, string description, StatusEnum status, string comment, List<File> files)
    {
        Sprint = sprint;
        Name = name;
        Description = description;
        Status = status;
        Comment = comment;
        Files = new List<File>();
        if(files != null)
            Files.AddRange(files);
    }

    public Sprint Sprint { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StatusEnum Status { get; set; }
    public string Comment { get; set; }
    public List<File> Files { get; set; }
}