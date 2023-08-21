using ImageLayerer.Application.Interfaces;
using ImageLayerer.Application.Models;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;

namespace ImageLayerer.Application.Services;

public class ImageSourceService : IImageSourceService
{
    private readonly IMemoryCache memoryCache;
    private readonly IRemoteFileService remoteFileService;
    private readonly ILocalFileService localFileService;
    private readonly IAzureService azureService;

    public ImageSourceService(
        IMemoryCache memoryCache,
        IRemoteFileService remoteFileService,
        ILocalFileService localFileService,
        IAzureService azureService)
    {
        this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        this.remoteFileService = remoteFileService ?? throw new ArgumentNullException(nameof(remoteFileService));
        this.localFileService = localFileService ?? throw new ArgumentNullException(nameof(localFileService));
        this.azureService = azureService ?? throw new ArgumentNullException(nameof(azureService));
    }

    public async Task<ImageFile> GetImageAsync(ImageSource imageSource, CancellationToken cancellationToken)
    {
        var sourceHash = imageSource.CalculateHash();

        if (imageSource.Refresh || !memoryCache.TryGetValue(sourceHash, out ImageFile cachedImage))
        {
            var imageAsBytes = await FetchFileAsync(imageSource, cancellationToken);

            string filename = Path.GetFileName(imageSource.SourcePath);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filename, out string contentType))
            {
                throw new Exception();
            }


            ImageFile image = new()
            {
                FileName = filename,
                MimeType = contentType,
                Content = imageAsBytes
            };

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromDays(14));

            memoryCache.Set(sourceHash, image, cacheEntryOptions);
            return image;
        }

        return cachedImage;
    }

    public async Task<byte[]> FetchFileAsync(ImageSource imageSource, CancellationToken cancellationToken)
    {
        return imageSource.SourceType switch
        {
            Constants.SourceTypes.Local => await localFileService.GetLocalFileAsync(imageSource.SourcePath, cancellationToken),
            Constants.SourceTypes.Remote => await remoteFileService.GetRemoteFileAsync(imageSource.SourcePath, cancellationToken),
            Constants.SourceTypes.Azure => await azureService.GetAzureFileAsync(imageSource.SourcePath, cancellationToken),
            _ => throw new Exception(),
        };
    }
}
