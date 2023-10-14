using Ardalis.GuardClauses;

namespace Domain.Formulas;

public class Title
{
    private string _value;

    public string Value
    {
        get => _value;
        private set
        {
            value = value.Trim();
            if (value.Length < 10)
                throw new ArgumentException($"Title must be at least 10 characters! was {value.Length}");
            _value = Guard.Against.NullOrWhiteSpace(value);
        }
    }

    public Title(string value)
    {
        Value = value;
    }
}