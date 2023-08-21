using ImageLayerer.Application.Interfaces;
using ImageLayerer.Application.Models;
using Microsoft.AspNetCore.StaticFiles;
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
