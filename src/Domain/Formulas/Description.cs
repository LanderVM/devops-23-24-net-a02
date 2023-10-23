using Domain.Common;

namespace Domain.Formulas;

public class Description : Entity
{
  private string _subtext = "Er is geen uitleg beschikbaar voor dit item.";

  private string _title = "Er is geen titel beschikbaar voor dit item.";
  private Description() { } // EF Core constructor TODO figure out if this could/should be in same table as parent obj?

  public Description(string title,
    string subtext)
  {
    Title = title;
    Subtext = subtext;
  }

  public string Title
  {
    get => _title;
    private set
    {
      value = Guard.Against.NullOrWhiteSpace(value);
      _title = Guard.Against.NullOrWhiteSpace(value);
    }
  }

  public string Subtext
  {
    get => _subtext;
    private set
    {
      value = Guard.Against.NullOrWhiteSpace(value);
      _subtext = Guard.Against.NullOrWhiteSpace(value);
    }
  }
}
