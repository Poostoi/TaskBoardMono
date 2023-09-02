namespace Models.Board;

public class Project: BaseEntity
{
    public Project(string name, string description)
    {
        Name = name;
        Description = description;
    }

    private Project()
    {
    }

    public string Name { get; set; }
    public string Description { get; set; }
}