namespace ImageLayerer.Application.Interfaces;

public interface ILocalFileService
{
    Task<byte[]> GetLocalFileAsync(string path);
}
