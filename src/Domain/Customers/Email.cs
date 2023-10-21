﻿using System.Net.Mail;
using Domain.Common;

namespace Domain.Customers;

public class Email : Entity
{
  private Email() { } // EF Core constructor

  public Customer Customer { get; set; }

  public MailAddress Value { get; }

  public Email(string value)
  {
    Guard.Against.NullOrWhiteSpace(value);
    if (IsValidEmail(value))
    {
      Value = new MailAddress(value);
    }
    else
    {
      throw new ArgumentException($"{value} is an invalid email");
    }
  }

  private bool IsValidEmail(string email)
  {
    string trimmedEmail = email.Trim();

    if (trimmedEmail.EndsWith("."))
    {
      return false;
    }

    try
    {
      var addr = new MailAddress(email);
      return addr.Address == trimmedEmail;
    }
    catch
    {
      return false;
    }
  }
}
