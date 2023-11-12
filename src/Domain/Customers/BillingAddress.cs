using Domain.Common;
using Domain.Quotations;

namespace Domain.Customers;

public class BillingAddress
{
  private BillingAddress() { } // EF Core constructor

  public BillingAddress(string street, string houseNumber, string city, string postalCode)
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

  public override string ToString()
  {
    return $"{Street} {HouseNumber}, {PostalCode} {City}";
  }
}
