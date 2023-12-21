using System.Text.RegularExpressions;
using devops_23_24_net_a02.Domain.Common;

namespace Domain.Customers;

public partial class VatNumber : ValueObject
{
  private VatNumber() { } // EF Core constructor

  public VatNumber(string value)
  {
    if (IsValidVatNumber(value))
    {
      Value = Guard.Against.NullOrWhiteSpace(value);
    }
    else
    {
      throw new ArgumentException($"{value} is not a valid vat number!");
    }
  }

  public string Value { get; } = default!;

  private static bool IsValidVatNumber(string value)
  {
    return BelgianVatRegex().IsMatch(value.Trim());
  }

  [GeneratedRegex("^BE[01][0-9]{9}$")]
  private static partial Regex BelgianVatRegex();

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value;
  }
}
