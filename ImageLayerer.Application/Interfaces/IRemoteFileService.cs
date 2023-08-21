namespace ImageLayerer.Application.Interfaces;

public interface IRemoteFileService
{
    Task<byte[]> GetRemoteFileAsync(string filename);
}
