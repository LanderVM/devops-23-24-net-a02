using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Customers;

public class PhoneNumber
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
}
