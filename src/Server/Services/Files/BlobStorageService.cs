using System;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using devops_23_24_net_a02.Domain.Files;
using HeyRed.Mime;
using Microsoft.Extensions.Configuration;

namespace devops_23_24_net_a02.Services.Files;

public class BlobStorageService : IStorageService
{
  private readonly string connectionString;

  public Uri BasePath => new Uri("https://a2blanchestorage.blob.core.windows.net/images");

  public BlobStorageService(IConfiguration configuration)
  {
    connectionString = configuration.GetConnectionString("Storage");
  }

  public Uri GenerateImageUploadSas(Image image)
  {
    string containerName = "images";
    var blobServiceClient = new BlobServiceClient(connectionString);
    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    BlobClient blobClient = containerClient.GetBlobClient(image.Filename);

    var blobSasBuilder = new BlobSasBuilder
    {
      ExpiresOn = DateTime.UtcNow.AddMinutes(5),
      BlobContainerName = containerName,
      BlobName = image.Filename,
    };

    blobSasBuilder.SetPermissions(BlobSasPermissions.Create | BlobSasPermissions.Write);
    var sas = blobClient.GenerateSasUri(blobSasBuilder);
    return sas;
  }
}

