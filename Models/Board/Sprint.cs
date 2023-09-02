namespace Models.Board;

public class Sprint: BaseEntity
{
    public Sprint(Project project, string name, string description, DateTime dateStart, DateTime dateEnd, string comment, List<File> files)
    {
        Project = project;
        Name = name;
        Description = description;
        DateStart = dateStart;
        DateEnd = dateEnd;
        Comment = comment;
        Files = files;
        if(files != null)
            Files.AddRange(files);
    }

    public Project Project { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Comment { get; set; }
    public List<File> Files { get; set; }
}