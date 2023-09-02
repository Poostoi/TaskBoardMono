using Microsoft.EntityFrameworkCore;
using Models.Board;
using Models.UserActivity;
using Task = Models.Board.Task;

namespace Providers;

public class ApplicationContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<Task> Tasks { get; set; }
    
    
    public ApplicationContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;" +
                                 "Port=5432;" +
                                 "Database=DBTaskBoard;" +
                                 "Username=postgres;" +
                                 "Password=37242");
    }
}