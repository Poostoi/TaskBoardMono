using File = Models.Board.File;

namespace Services.Providers;

public interface IFileProvider
{
    Task CreateAsync(List<File> files, CancellationToken cancellationToken);
}