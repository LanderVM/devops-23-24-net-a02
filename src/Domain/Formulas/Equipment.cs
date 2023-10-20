using Domain.Common;

namespace Domain.Formulas;

public class Equipment
{
  public Image Image { get; set; }
  public Description Description { get; set; }

  private decimal _price = 0;

  public decimal Price
  {
    get => _price;
    set
    {
      _price = Guard.Against.Negative(value);
      
    }
  }

  private int _stock = 0;

  public int Stock
  {
    get => _stock;
    set
    {
      _stock = Guard.Against.Negative(value);
    }
  }

  public Equipment(Image image, string title, string description, decimal price, int stock)
  {
    Image = image;
    Description = new Description(title, description);
    Price = price;
    Stock = stock;
  }
}
