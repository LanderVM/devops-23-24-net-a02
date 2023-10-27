using Domain.Common;

namespace Domain.Formulas;

public class Formula : Entity
{
  private Formula() { } // EF Core constructor

  public Formula(List<Equipment> equipment, string title, string description)
  {
    Equipment.AddRange(equipment);
    Description = new Description(title, description);
  }

  public List<Equipment> Equipment { get; } = new();

  public Description Description { get; set; } = default!;
}
