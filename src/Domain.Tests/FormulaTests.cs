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

    formula.Equipment.Count.ShouldBe(0);
  }

  [Fact]
  public void Create_new_formula_with_equipment_happyFlow()
  {
    List<Equipment> equipment = new();
    Image image = new Image("https://blazor.radzen.com/images/community.svg", "Placeholder image");
    equipment.Add(new Equipment(image, "BBQ Deluxe", "Tasty barbecue stuff, in a deluxe package!", 100M, 2));
    equipment.Add(new Equipment(image, "Tent Decoration", "Tents for a rainy day. Or perhaps for when it's too hot to sit in the sun?", 35.99M, 21));
    const string title = "The extended food truck formula";
    const string description =
        "Celebrating the new academic year? Sunny or rainy, this formula takes care of your students!";

    Formula formula = new Formula(equipment, title, description);

    formula.Equipment.Count.ShouldBe(equipment.Count);
    formula.Equipment.ShouldContain(equipment[0]);
    formula.Equipment.ShouldContain(equipment[1]);
  }

  [Theory]
  [InlineData("")]
  [InlineData("    ")]
  [InlineData(null)]
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
  [InlineData(null)]
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
