using devops_23_24_net_a02.Domain.Files;

namespace devops_23_24_net_a02.Services.Files;

public interface IStorageService
{
  Uri BasePath { get; }
  Uri GenerateImageUploadSas(Image image);
}
