using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace ImageLayerer.Application.Factories;

public class AzureBlobClientFactory
{
    private readonly string connectionString;

    public AzureBlobClientFactory(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("Azure");
    }

    public BlobContainerClient GetBlobContainerClient(string containerName)
    {
        var blobServiceClient = new BlobServiceClient(connectionString);
        return blobServiceClient.GetBlobContainerClient(containerName);
    }
}
