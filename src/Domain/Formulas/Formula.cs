using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Formulas;

public class Formula
{
  public List<Equipment> Equipment { get; } = new();

  public Description Description { get; set; }

  public Formula(List<Equipment> equipment, string title, string description)
  {
    Equipment.Add(new Equipment(
        new Image("https://blazor.radzen.com/images/community.svg", "Food truck placeholder img"),
        "The Food Truck",
        "Het feest begint en eindigt bij Project BLANCHE.")); // TODO replace with food truck obj from db once available
    Equipment.AddRange(equipment);
    Description = new Description(title, description);
  }
}
