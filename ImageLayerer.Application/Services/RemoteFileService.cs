using ImageLayerer.Application.Interfaces;

namespace ImageLayerer.Application.Services;

public class RemoteFileService : IRemoteFileService
{
    private readonly IHttpClientFactory httpClientFactory;

    public RemoteFileService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<byte[]> GetRemoteFileAsync(string url, CancellationToken cancellationToken)
    {
        var httpClient = httpClientFactory.CreateClient();
        HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            // Read the image content as bytes
            return await response.Content.ReadAsByteArrayAsync(cancellationToken);
        }

        throw new FileNotFoundException();
    }
}
