using Domain.Common;
using Domain.Quotations;

namespace Domain.Customers;

public class Customer : Entity
{
  private Customer() { } // EF Core constructor

  public Customer(string firstName, string lastName, Email email, Address billingAddress, PhoneNumber phoneNumber)
  {
    FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
    Email = email;
    BillingAddress = billingAddress;
    PhoneNumber = phoneNumber;
  }

  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;
  public Email Email { get; set; } = default!;
  public Address BillingAddress { get; set; } = default!;
  public PhoneNumber PhoneNumber { get; set; } = default!;
  public List<Quotation> Quotations { get; set; } = new();
}
