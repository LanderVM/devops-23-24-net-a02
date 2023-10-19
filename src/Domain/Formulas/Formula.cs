namespace Domain.Formulas;

public class Formula
{
  public List<Equipment> Equipment { get; } = new();

  public Description Description { get; set; }

  public Formula(List<Equipment> equipment, string title, string description)
  {
    Equipment.AddRange(equipment);
    Description = new Description(title, description);
  }
}
