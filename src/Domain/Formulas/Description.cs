﻿namespace Domain.Formulas;

public class Description
{
  private string _title = "Er is geen titel beschikbaar voor dit item.";

  public string Title
  {
    get => _title;
    private set
    {
      value = Guard.Against.NullOrWhiteSpace(value);
      _title = Guard.Against.NullOrWhiteSpace(value);
    }
  }

  private string _subtext = "Er is geen uitleg beschikbaar voor dit item.";

  public string Subtext
  {
    get => _subtext;
    private set
    {
      value = Guard.Against.NullOrWhiteSpace(value);
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
