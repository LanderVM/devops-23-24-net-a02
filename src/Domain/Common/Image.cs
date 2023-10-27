namespace Domain.Common;

public class Image : Entity
{
  private Image() { } // EF Core constructor

  public Image(string imageUrl, string altText /*, Equipment equipment*/)
  {
    ImageUrl = imageUrl;
    AltText = altText;
    /*Equipment = equipment;*/
  }

  public string ImageUrl { get; set; } = default!; // TODO blob storage
  public string AltText { get; set; } = default!;
}
