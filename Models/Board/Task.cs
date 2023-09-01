namespace Models.Board;

public class Task: BaseEntity
{
    public Sprint Sprint { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StatusEnum Status { get; set; }
    public string Comment { get; set; }
}