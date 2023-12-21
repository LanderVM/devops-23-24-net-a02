using Domain.Common;
using Domain.Quotations;

namespace Domain.Formulas;

public class Formula : Entity
{
  private Formula() { } // EF Core constructor

  public Formula(List<Equipment> equipment, string title, List<string> description, string imageUrl = "https://a2blanchestorage.blob.core.windows.net/images/SfeerFoto1.jpg")
  {
    Equipment.AddRange(equipment);
    Description = new Description(title, description);
    ImageUrl = Guard.Against.NullOrWhiteSpace(imageUrl);
  }

  public Formula(List<Equipment> equipment)
  {
    Equipment.AddRange(equipment);
  }

  public List<Equipment> Equipment { get; } = new();
  public Description Description { get; set; } = default!;
  public string ImageUrl { get; set; }
  public decimal PricePerDayExtra { get; set; } = 50M;

  public List<decimal> BasePrice { get; set; } = new() { 350, 450, 520 };

  public List<Quotation> OrderedIn { get; set; } = default!;

  public decimal GetPriceForEquipment() 
  { 
    return Equipment.Sum(x => x.Price); 
  }

  public List<QuotationLine> GetQuotationLines(int amountOfPeople)
  {
    List<QuotationLine> result = new();

    foreach (Equipment equipment in Equipment)
    {
      result.Add(new QuotationLine(equipment, amountOfPeople));
    }

    return result;
  }
}
