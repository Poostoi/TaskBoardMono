namespace Models.Board;

public class Sprint: BaseEntity
{
    public Project Project { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Comment { get; set; }
}