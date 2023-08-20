using Microsoft.Extensions.Caching.Memory;
using ImageLayerer.API.Models;
using Microsoft.AspNetCore.StaticFiles;

namespace ImageLayerer.API.Services;

public class ImageSourceService : IImageSourceService
{
    private readonly IMemoryCache memoryCache;
    private readonly IHttpClientFactory httpClientFactory;

    public ImageSourceService(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
    {
        this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<ImageFile> GetImageAsync(ImageSource imageSource)
    {
        if (!memoryCache.TryGetValue(imageSource.SourcePath, out ImageFile cachedImage))
        {
            var imageAsBytes = await (imageSource.SourceType == Constants.SourceTypes.Remote
                ? DownloadFileAsync(imageSource.SourcePath)
                : FetchLocalFileAsync(imageSource.SourcePath)
            );

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

            memoryCache.Set(imageSource.SourcePath, image, cacheEntryOptions);
            return image;
        }

        return cachedImage;
    }

    public async Task<byte[]> FetchLocalFileAsync(string path)
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

    public async Task<byte[]> DownloadFileAsync(string url)
    {
        var httpClient = httpClientFactory.CreateClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            // Read the image content as bytes
            return await response.Content.ReadAsByteArrayAsync();
        }

        throw new FileNotFoundException();
    }
}
