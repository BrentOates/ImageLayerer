using ImageLayerer.Application.Interfaces;

namespace ImageLayerer.Application.Services;

public class LocalFileService : ILocalFileService
{
    public async Task<byte[]> GetLocalFileAsync(string path, CancellationToken cancellationToken)
    {
        try
        {
            return await File.ReadAllBytesAsync(path, cancellationToken);
        }
        catch (Exception)
        {
            throw new FileNotFoundException();
        }
    }
}
