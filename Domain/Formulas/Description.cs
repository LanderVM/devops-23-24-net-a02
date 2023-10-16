using Ardalis.GuardClauses;

namespace Domain.Formulas;

public class Description
{
  private string _title = "Er is geen titel beschikbaar voor dit item.";

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

  private string _subtext = "Er is geen uitleg beschikbaar voor dit item.";

  public string Subtext
  {
    get => _subtext;
    private set
    {
      value = value.Trim();
      if (value.Length < 24)
        throw new ArgumentException($"Description must be at least 24 characters! was {value.Length}");
      _subtext = Guard.Against.NullOrWhiteSpace(value);
    }
  }

  public Description(string title,
      string subtext)
  {
    Title = title;
    Subtext = subtext;
  }
}
