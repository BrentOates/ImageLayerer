using ImageLayerer.Application.Interfaces;
namespace ImageLayerer.Application.Services;

public class LocalFileService : ILocalFileService
{
    public async Task<byte[]> GetLocalFileAsync(string path)
    {
        try
        {
            return await File.ReadAllBytesAsync(path);
        }
        catch (Exception)
        {
            throw new FileNotFoundException();
        }
    }
}
