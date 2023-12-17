using Domain.Common;
using Domain.Quotations;

namespace Domain.Formulas;

public class Formula : Entity
{
  private Formula() { } // EF Core constructor

  public Formula(List<Equipment> equipment, string title, List<string> description)
  {
    Equipment.AddRange(equipment);
    Description = new Description(title, description);
  }

  public Formula(List<Equipment> equipment)
  {
    Equipment.AddRange(equipment);
  }

  public List<Equipment> Equipment { get; } = new();
  public Description Description { get; set; } = default!;
  public decimal PricePerDayExtra { get; set; } = 50M;

  public List<decimal> BasePrice { get; set; } = new() { 350, 450, 520 };

  public List<Quotation> OrderedIn { get; set; } = default!;

  public decimal getPriceForEquipment() 
  { 
    return Equipment.Sum(x => x.Price); 
  }

  public List<QuotationLine> getQuotationLines(int amountOfPeople)
  {
    List<QuotationLine> result = new();

    foreach (Equipment equipment in Equipment)
    {
      result.Add(new QuotationLine(equipment, amountOfPeople));
    }

    return result;
  }
}
