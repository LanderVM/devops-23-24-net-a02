using Domain.Common;

namespace Domain.Formulas;

public class Equipment
{
  public Image Image { get; set; }
  public Description Description { get; set; }

  public Equipment(Image image, string title, string description)
  {
    Image = image;
    Description = new Description(title, description);
  }
}
