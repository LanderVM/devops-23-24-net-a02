using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Customers;

public class PhoneNumber
{
  private PhoneNumber() { } // EF Core constructor

  public PhoneNumber(string value)
  {
    if (isValidPhoneNumber(value))
    {
      Value = Guard.Against.NullOrWhiteSpace(value);
    }
    else
    {
      throw new ArgumentException($"{value} is not a valid phone number!");
    }
  }

  public string Value { get; } = default!;

  private bool isValidPhoneNumber(string value)
  {
    return new PhoneAttribute().IsValid(value);
  }
}
