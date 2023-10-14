using Domain.Common;
using Domain.Formulas;
using Shouldly;

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
        equipment.Add(new Equipment(image, "BBQ", "Tasty barbecue stuff!"));
        equipment.Add(new Equipment(image, "Tent Decoration", "Tents for a rainy day."));
        const string title = "";
        const string description =
            "Celebrating the new academic year? Sunny or rainy, this formula takes care of your students!";

        Formula formula = new Formula(equipment, title, description);

        formula.Equipment.Count.ShouldBe(equipment.Count + 1);
        formula.Equipment.ShouldContain(equipment[0]);
        formula.Equipment.ShouldContain(equipment[1]);
    }
}