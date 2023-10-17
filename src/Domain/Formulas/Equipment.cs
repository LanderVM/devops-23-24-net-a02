using Domain.Common;

namespace Domain.Formulas;

public class Equipment
{
  public Image Image { get; set; }
  public Description Description { get; set; }
  /*public decimal Price { get; set; }
  public int Amount { get; set; }*/

  public Equipment(Image image, string title, string description/*, decimal price, int amount*/)
  {
    Image = image;
    Description = new Description(title, description);
    /*Price = price;
    Amount = amount;*/
  }
}
