using System.ComponentModel.DataAnnotations;
using devops_23_24_net_a02.Domain.Common;
using Domain.Common;

namespace Domain.Customers;

public class PhoneNumber : ValueObject
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

  private bool IsValidPhoneNumber(string value)
  {
    return new PhoneAttribute().IsValid(value);
  }

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value;
  }
}
