using System.Text.RegularExpressions;
using devops_23_24_net_a02.Domain.Common;

namespace Domain.Customers;

public partial class PhoneNumber : ValueObject
{
  private PhoneNumber() { } // EF Core constructor

  public PhoneNumber(string value)
  {
    if (IsValidPhoneNumber(value))
    {
      Value = Guard.Against.NullOrWhiteSpace(value);
    }
    else
    {
      throw new ArgumentException($"{value} is not a valid phone number!");
    }
  }

  public string Value { get; } = default!;

  private static bool IsValidPhoneNumber(string value)
  {
    return BelgianPhoneRegex().IsMatch(value);
  }

  [GeneratedRegex(@"^(?:\+32|0)4\d{8}$")]
  private static partial Regex BelgianPhoneRegex();

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value;
  }
}
