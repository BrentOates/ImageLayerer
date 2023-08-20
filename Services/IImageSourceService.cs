using ImageLayerer.API.Models;

namespace ImageLayerer.API.Services;

public interface IImageSourceService
{
    Task<ImageFile> GetImageAsync(ImageSource imageSource);

    Task<byte[]> FetchLocalFileAsync(string path);

    Task<byte[]> DownloadFileAsync(string url);
}
