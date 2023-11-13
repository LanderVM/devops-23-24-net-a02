using Domain.Common;
using Domain.Formulas;

namespace Domain.Tests;

public class FormulaTests
{
  List<Equipment> equipment = new();
  const string title = "The base food truck formula";

  private static readonly List<string> description =
    new() { "Having a small party? Our iconic food truck is your choice of the evening!" };

  [Fact]
  public void Create_new_formula_without_equipment_happyFlow()
  {
    var formula = new Formula(equipment, title, description);

    formula.Equipment.Count.ShouldBe(0);
  }

  [Fact]
  public void Create_new_formula_with_equipment_happyFlow()
  {
    equipment.Add(new Equipment("BBQ Deluxe", new List<string> { "Tasty barbecue stuff, in a deluxe package!" }, 100M,
      2));
    equipment.Add(new Equipment("Tent Decoration",
      new List<string> { "Tents for a rainy day. Or perhaps for when it's too hot to sit in the sun?" }, 35.99M, 21));

    var formula = new Formula(equipment, title, description);

    formula.Equipment.Count.ShouldBe(equipment.Count);
    formula.Equipment.ShouldContain(equipment[0]);
    formula.Equipment.ShouldContain(equipment[1]);
  }

  [Theory]
  [InlineData("")]
  [InlineData("    ")]
  [InlineData(null)]
  public void Create_new_formula_invalid_title(string invalidTitle)
  {
    Should.Throw<ArgumentException>(() =>
    {
      new Formula(equipment, invalidTitle, description);
    });
  }

  [Theory]
  [InlineData("")]
  [InlineData("    ")]
  [InlineData(null)]
  public void Create_new_formula_description_invalid(string invalidDescription)
  {
    Should.Throw<ArgumentException>(() =>
    {
      new Formula(equipment, title, new List<string> { invalidDescription });
    });
  }

  [Theory]
  [InlineData(-50)]
  [InlineData(int.MinValue)]
  public void Create_new_formula_stock_invalid(int stock)
  {
    Should.Throw<ArgumentException>(() =>
    {
      equipment.Add(new Equipment(
        "BBQ Deluxe",
        new List<string> { "Tasty barbecue stuff, in a deluxe package!" },
        20.50M,
        stock));
      new Formula(equipment, title, description);
    });
  }

  [Theory]
  [InlineData("-50")]
  [InlineData("-23.50")]
  [InlineData("-0.0000000001")]
  [InlineData("-70.23581629000")]
  public void Create_new_formula_equipmentPrice_invalid(string number)
  {
    var price = Convert.ToDecimal(number);
    Should.Throw<ArgumentException>(() =>
    {
      equipment.Add(new Equipment("BBQ Deluxe",
        new List<string> { "Tasty barbecue stuff, in a deluxe package!" }, price, 50));
      new Formula(equipment, title, description);
    });
  }
}
