using Domain.Common;

namespace Domain.Customers;

public class Customer : Entity
{
  private Customer() { } // EF Core constructor

  public Customer(string firstName, string lastName, Email email, Address address, PhoneNumber phoneNumber)
  {
    FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
    Email = email;
    Address = address;
    PhoneNumber = phoneNumber;
  }

  public string FirstName { get; } = default!;
  public string LastName { get; } = default!;
  public Email Email { get; } = default!;
  public Address Address { get; } = default!;
  public PhoneNumber PhoneNumber { get; } = default!;
}
