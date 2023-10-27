using Domain.Common;

namespace Domain.Customers;

public class Address
{
  private Address() { } // EF Core constructor

  public Address(string street, string houseNumber, string city, string postalCode)
  {
    Street = Guard.Against.NullOrWhiteSpace(street, nameof(street));
    HouseNumber = Guard.Against.NullOrWhiteSpace(houseNumber, nameof(houseNumber));
    City = Guard.Against.NullOrWhiteSpace(city, nameof(city));
    PostalCode = Guard.Against.NullOrWhiteSpace(postalCode, nameof(postalCode));
  }

  public string Street { get; } = default!;
  public string HouseNumber { get; } = default!;
  public string City { get; } = default!;
  public string PostalCode { get; } = default!;

  public override string ToString()
  {
    return $"{Street} {HouseNumber}, {PostalCode} {City}";
  }
}
