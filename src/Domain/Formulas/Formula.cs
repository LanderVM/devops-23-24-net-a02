using Domain.Common;
using Domain.Quotations;

namespace Domain.Formulas;

public class Formula : Entity
{
  private Formula() { } // EF Core constructor

  public Formula(List<Equipment> equipment, string title, string description, decimal pricePerDay)
  {
    Equipment.AddRange(equipment);
    Description = new Description(title, description);
    PricePerDay = pricePerDay;
  }

  public List<Equipment> Equipment { get; } = new();
  public Description Description { get; set; } = default!;
  private decimal _pricePerDay;

  public decimal PricePerDay
  {
    get => _pricePerDay;
    set
    {
      _pricePerDay = Guard.Against.Negative(value);
    }
  }

  public List<Quotation> OrderedIn { get; set; } = default!;
}
