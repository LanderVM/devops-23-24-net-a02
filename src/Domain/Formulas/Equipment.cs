using Domain.Common;

namespace Domain.Formulas;

public class Equipment : Entity
{
  private decimal _price;
  private int _stock;

  private Equipment() { } // EF Core constructor

  public Equipment(string title, List<string> description, decimal price, int stock)
  {
    Description = new Description(title, description);
    Price = price;
    Stock = stock;
  }
  public Equipment(decimal price)
  {
    // Description = new Description(title, description);
    Price = price;
  }


  public List<Formula> Formulas { get; set; } = default!;

  public Description Description { get; set; } = default!;

  public decimal Price
  {
    get => _price;
    set => _price = Guard.Against.Negative(value);
  }

  public int Stock
  {
    get => _stock;
    set => _stock = Guard.Against.Negative(value);
  }
}
