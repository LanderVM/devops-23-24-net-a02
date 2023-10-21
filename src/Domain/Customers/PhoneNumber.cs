using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Customers;

public class PhoneNumber : Entity
{
  private PhoneNumber() { } // EF Core constructor

  public Customer Customer { get; set; }
  public string Value { get; }

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

  private bool isValidPhoneNumber(string value)
  {
    return new PhoneAttribute().IsValid(value);
  }
}
