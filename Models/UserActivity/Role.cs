namespace Models.UserActivity;

public class Role: BaseEntity
{
    public Role(string name)
    {
        Name = name;
    }
    protected Role(){}

    public string Name { get; set; }
}