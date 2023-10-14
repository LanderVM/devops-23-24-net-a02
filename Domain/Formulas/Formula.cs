using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Formulas;

public class Formula
{
    public List<Equipment> Equipment { get; } = new();

    private string _title;

    public string Title
    {
        get => _title;
        private set
        {
            value = value.Trim();
            if (value.Length < 10)
                throw new ArgumentException($"Title must be at least 10 characters! was {value.Length}");
            _title = Guard.Against.NullOrWhiteSpace(value);
        }
    }

    private string _description;

    public string Description
    {
        get => _description;
        private set
        {
            value = value.Trim();
            if (value.Length < 24)
                throw new ArgumentException($"Desciption must be at least 24 characters! was {value.Length}");
            _description = Guard.Against.NullOrWhiteSpace(value);
        }
    }

    public Formula(List<Equipment> equipment, string title, string description)
    {
        Equipment.Add(new Equipment(
            new Image("https://blazor.radzen.com/images/community.svg", "Food truck placeholder img"),
            "FoodTruck",
            "The party begins here with Blanche's flagship FoodTruck"));
        foreach (var eq in equipment)
            Equipment.Add(eq);

        Title = title;
        Description = description;
    }
}