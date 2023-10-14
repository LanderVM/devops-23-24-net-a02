using Domain.Formulas;
using Shouldly;

namespace FormulaTests;

public class FormulaTests
{
    [Fact]
    public void Create_new_formula_only_has_foodtruck()
    {
        List<Equipment> equipment = new();
        string title = "", description = "";

        Formula formula = new Formula(equipment, title, description);

        formula.Equipment.Count.ShouldBe(1);
    }
}