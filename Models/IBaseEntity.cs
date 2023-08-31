namespace Models;

public interface IBaseEntity
{
    Guid Id { get; set; }
    DateTime DateCreate { get; set; }
    DateTime DateUpdate { get; set; }
}