using Domain.Common;
using Domain.Formulas;

namespace FormulaTests;

public class FormulaTests
{
  [Fact]
  public void Create_new_formula_without_equipment_happyFlow()
  {
    List<Equipment> equipment = new();
    const string title = "The base food truck formula";
    const string description = "Having a small party? Our iconic food truck is your choice of the evening!";

    Formula formula = new Formula(equipment, title, description);

    formula.Equipment.Count.ShouldBe(1);
  }

  [Fact]
  public void Create_new_formula_with_equipment_happyFlow()
  {
    List<Equipment> equipment = new();
    Image image = new Image("https://blazor.radzen.com/images/community.svg", "Placeholder image");
    equipment.Add(new Equipment(image, "BBQ Deluxe", "Tasty barbecue stuff, in a deluxe package!"));
    equipment.Add(new Equipment(image, "Tent Decoration", "Tents for a rainy day. Or perhaps for when it's too hot to sit in the sun?"));
    const string title = "The extended food truck formula";
    const string description =
        "Celebrating the new academic year? Sunny or rainy, this formula takes care of your students!";

    Formula formula = new Formula(equipment, title, description);

    formula.Equipment.Count.ShouldBe(equipment.Count + 1);
    formula.Equipment.ShouldContain(equipment[0]);
    formula.Equipment.ShouldContain(equipment[1]);
  }

  [Theory]
  [InlineData("")]
  [InlineData("    ")]
  [InlineData("the")] // < 10 char
  [InlineData("not quite      ")] // >= 10 char, but not after it gets trimmed
  [InlineData("   not quite")] // >= 10 char, but not after it gets trimmed
  public void Create_new_formula_invalid_title(string title)
  {
    List<Equipment> equipment = new();
    const string description =
        "Celebrating the new academic year? Sunny or rainy, this formula takes care of your students!";


    Should.Throw<ArgumentException>(() =>
    {
      Formula formula = new Formula(equipment, title, description);
    });
  }

  [Theory]
  [InlineData("")]
  [InlineData("    ")]
  [InlineData("the best party exp")] // < 24 char
  [InlineData("the best party experien    ")] // >= 24 char, but not after it gets trimmed
  [InlineData("      the best party experien")] // >= 24 char, but not after it gets trimmed
  public void Create_new_formula_description_invalid(string description)
  {
    List<Equipment> equipment = new();
    const string title = "long enough title";

    Should.Throw<ArgumentException>(() =>
    {
      Formula formula = new Formula(equipment, title, description);
    });
  }
}
