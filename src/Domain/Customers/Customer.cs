using Domain.Common;
using Domain.Quotations;

namespace Domain.Customers;

public class Customer : Entity
{
  private Customer() { } // EF Core constructor

  public Customer(string firstName, string lastName, Email email, BillingAddress billingAddress,
    PhoneNumber phoneNumber, VatNumber? vatNumber)
  {
    FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
    Email = email;
    BillingAddress = billingAddress;
    PhoneNumber = phoneNumber;
    VatNumber = vatNumber;
  }

  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;
  public VatNumber? VatNumber { get; set; }
  public Email Email { get; set; } = default!;
  public BillingAddress BillingAddress { get; set; } = default!;
  public PhoneNumber PhoneNumber { get; set; } = default!;
  public List<Quotation> Quotations { get; set; } = new();
}
