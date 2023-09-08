using Microsoft.Extensions.FileProviders;
using Services.Providers;
using IFileProvider = Services.Providers.IFileProvider;

namespace Providers.Providers;

public class FileProvider : IFileProvider
{
    private readonly ApplicationContext _applicationContext;

    public FileProvider(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }


    public async Task CreateAsync(List<Models.Board.File> files, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(files);

        await _applicationContext.AddRangeAsync(files, cancellationToken);

        await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}