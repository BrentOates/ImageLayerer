namespace ImageLayerer.Application.Interfaces;

public interface IAzureService
{
    Task<byte[]> GetAzureFileAsync(string filename);
}
