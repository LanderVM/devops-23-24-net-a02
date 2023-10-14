using Ardalis.GuardClauses;

namespace Domain.Formulas;

public class Description
{
    private string _value;

    public string Value
    {
        get => _value;
        private set
        {
            value = value.Trim();
            if (value.Length < 24)
                throw new ArgumentException($"Desciption must be at least 24 characters! was {value.Length}");
            _value = Guard.Against.NullOrWhiteSpace(value);
        }
    }

    public Description(string value)
    {
        Value = value;
    }
}