using Domain.Formulas;

namespace Domain.Common;

public class Image : Entity
{
  private Image() { } // EF Core constructor

  public string ImageUrl { get; set; } // TODO blob storage
  public string AltText { get; set; }

  public Equipment Equipment { get; set; } // TODO only for equipment or for other entity's too?

  public Image(string imageUrl, string altText/*, Equipment equipment*/)
  {
    ImageUrl = imageUrl;
    AltText = altText;
    /*Equipment = equipment;*/
  }
}
