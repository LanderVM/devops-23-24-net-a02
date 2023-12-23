using devops_23_24_net_a02.Domain.Common;
using HeyRed.Mime;

namespace devops_23_24_net_a02.Domain.Files;

public class Image : ValueObject
{
  public Image(Uri basePath, string contentType)
  {
    Identifier = Guid.NewGuid();
    Extension = MimeTypesMap.GetExtension(contentType).ToLower();
    BasePath = Guard.Against.Null(basePath, nameof(basePath));
  }

  public Uri BasePath { get; }
  public Guid Identifier { get; }
  public string Extension { get; }

  public string Filename => $"{Identifier}.{Extension}";
  public Uri FileUri => new($"{BasePath}/{Filename}");

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Extension.ToLower();
    yield return Identifier;
    yield return BasePath;
  }
}
