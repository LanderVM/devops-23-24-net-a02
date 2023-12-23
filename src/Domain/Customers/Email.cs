using System.Net.Mail;
using Domain.Common;

namespace Domain.Customers;

public class Email : Entity
{
  private Email() { } // EF Core constructor

  public Email(string value)
  {
    var trimmedEmail = value.Trim();
    Guard.Against.NullOrWhiteSpace(trimmedEmail);
    if (trimmedEmail.EndsWith("."))
    {
      throw new ArgumentException($"{value} may not end with a dot", nameof(value));
    }

    var isValidAddress = MailAddress.TryCreate(trimmedEmail, out var parsedEmail);
    if (isValidAddress)
    {
      Value = parsedEmail!.ToString();
    }
    else
    {
      throw new ArgumentException($"{value} is an invalid email", nameof(value));
    }
  }

  public string Value { get; set; } = default!;
}
