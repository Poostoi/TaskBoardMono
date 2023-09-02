namespace Models.Board;

public class File: BaseEntity
{
    public File(string name, byte[] dataImage, Sprint? sprint, Task? task)
    {
        Name = name;
        DataImage = dataImage;
        Sprint = sprint;
        Task = task;
    }

    private File()
    {
    }

    public string Name { get; set; }
    public byte[] DataImage { get; set; }
    public Sprint? Sprint { get; set; }
    public Task? Task { get; set; }
}