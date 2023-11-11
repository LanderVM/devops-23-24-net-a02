using Domain.Common;
using Domain.Quotations;

namespace Domain.Customers;

public class Address : Entity
{
  private Address() { } // EF Core constructor

  public Address(string street, string houseNumber, string city, string postalCode)
  {
    Street = Guard.Against.NullOrWhiteSpace(street, nameof(street));
    HouseNumber = Guard.Against.NullOrWhiteSpace(houseNumber, nameof(houseNumber));
    City = Guard.Against.NullOrWhiteSpace(city, nameof(city));
    PostalCode = Guard.Against.NullOrWhiteSpace(postalCode, nameof(postalCode));
  }

  public string Street { get; set; } = default!;
  public string HouseNumber { get; set; } = default!;
  public string City { get; set; } = default!;
  public string PostalCode { get; set; } = default!;
  public List<Quotation> EventLocations { get; set; } = new();
  public List<Customer> BillingAddresses { get; set; } = new();

  public override string ToString()
  {
    return $"{Street} {HouseNumber}, {PostalCode} {City}";
  }
}
