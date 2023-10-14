using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Formulas;

public class Formula
{
    public List<Equipment> Equipment { get; } = new();

    public Title Title { get; set; }

    public Description Description { get; set; }

    public Formula(List<Equipment> equipment, string title, string description)
    {
        Equipment.Add(new Equipment(
            new Image("https://blazor.radzen.com/images/community.svg", "Food truck placeholder img"),
            "The Food Truck",
            "The party begins here with Blanche's flagship FoodTruck")); // TODO replace with foodtruck obj from db once available
        foreach (var eq in equipment)
            Equipment.Add(eq);

        Title = new Title(title);
        Description = new Description(description);
    }
}