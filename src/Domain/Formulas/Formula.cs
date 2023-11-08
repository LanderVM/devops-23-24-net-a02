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
  public decimal PricePerDay { get; set; } = default!;
  public List<Quotation> OrderedIn { get; set; } = default!;
}
