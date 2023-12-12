using Domain.Common;

namespace Domain.Formulas;

public class Equipment : Entity
{
  private decimal _price;
  private int _stock;
  private string _imageUrl = default!;

  private Equipment() { } // EF Core constructor

  public Equipment(string title, List<string> description, decimal price, int stock, string imageUrl = "https://via.placeholder.com/350x300")
  {
    Description = new Description(title, description);
    Price = price;
    Stock = stock;
    ImageUrl = imageUrl;
  }

  public Equipment(string title, decimal price)
  {
    Description = new Description(title, new List<string>());
    Price = price;
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

  public string ImageUrl
  {
    get => _imageUrl;
    set => _imageUrl = Guard.Against.NullOrWhiteSpace(value, nameof(ImageUrl));
  }
}
