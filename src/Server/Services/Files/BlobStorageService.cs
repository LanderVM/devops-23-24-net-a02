﻿using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using devops_23_24_net_a02.Domain.Files;

namespace devops_23_24_net_a02.Services.Files;

public class BlobStorageService : IStorageService
{
  private readonly string connectionString;

  public BlobStorageService(IConfiguration configuration)
  {
    connectionString = configuration.GetConnectionString("Storage");
  }

  public Uri BasePath => new("https://a2blanchestorage.blob.core.windows.net/images");

  public Uri GenerateImageUploadSas(Image image)
  {
    var containerName = "images";
    var blobServiceClient = new BlobServiceClient(connectionString);
    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    var blobClient = containerClient.GetBlobClient(image.Filename);

    var blobSasBuilder = new BlobSasBuilder
    {
      ExpiresOn = DateTime.UtcNow.AddMinutes(5), BlobContainerName = containerName, BlobName = image.Filename
    };

    blobSasBuilder.SetPermissions(BlobSasPermissions.Create | BlobSasPermissions.Write);
    var sas = blobClient.GenerateSasUri(blobSasBuilder);
    return sas;
  }
}
