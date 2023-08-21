using ImageLayerer.Application.Factories;
using ImageLayerer.Application.Interfaces;

namespace ImageLayerer.Application.Services;

public class AzureService : IAzureService
{
    private readonly AzureBlobClientFactory azureBlobClientFactory;

    public AzureService(AzureBlobClientFactory azureBlobClientFactory)
    {
        this.azureBlobClientFactory = azureBlobClientFactory ?? throw new ArgumentNullException(nameof(azureBlobClientFactory));
    }

    public async Task<byte[]> GetAzureFileAsync(string filename, CancellationToken cancellationToken)
    {
        var client = azureBlobClientFactory.GetBlobContainerClient("images");
        var blobClient = client.GetBlobClient(filename);
        var blobDownloadInfo = await blobClient.DownloadContentAsync(cancellationToken);
        byte[] blobContent = blobDownloadInfo.Value.Content.ToArray();
        return blobContent;
    }
}
