using devops_23_24_net_a02.Domain.Common;
using Domain.Common;
using Domain.Quotations;

namespace Domain.Customers;

public class BillingAddress : ValueObject
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

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Street;
    yield return HouseNumber;
    yield return City;
    yield return PostalCode;
  }
}
