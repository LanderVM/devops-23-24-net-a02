namespace Domain.Formulas;

public class Description
{
  private List<string> _attributes = new();

  private string _title = default!;
  private Description() { } // EF Core constructor

  public Description(string title,
    List<string> attributes)
  {
    Title = title;
    Attributes = attributes;
  }

  public string Title
  {
    get => _title;
    private set => _title = Guard.Against.NullOrWhiteSpace(value);
  }

  public List<string> Attributes
  {
    get => _attributes;
    protected set => _attributes = value.Select(attribute => Guard.Against.NullOrWhiteSpace(attribute)).ToList();
  }
}
