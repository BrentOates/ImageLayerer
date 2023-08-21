using ImageLayerer.Application.Models;

namespace ImageLayerer.Application.Interfaces;

public interface IImageSourceService
{
    Task<ImageFile> GetImageAsync(ImageSource imageSource, CancellationToken cancellationToken);
}
