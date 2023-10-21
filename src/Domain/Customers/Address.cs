using Domain.Common;

namespace Domain.Customers;

public class Address : Entity
{
  private Address() { } // EF Core constructor

  public Customer Customer { get; set; }
  public string Street { get; }
  public string HouseNumber { get; }
  public string City { get; }
  public string PostalCode { get; }

  public Address(string street, string houseNumber, string city, string postalCode)
  {
    Street = Guard.Against.NullOrWhiteSpace(street, nameof(street));
    HouseNumber = Guard.Against.NullOrWhiteSpace(houseNumber, nameof(houseNumber));
    City = Guard.Against.NullOrWhiteSpace(city, nameof(city));
    PostalCode = Guard.Against.NullOrWhiteSpace(postalCode, nameof(postalCode));
  }

  public override string ToString()
  {
    return $"{Street} {HouseNumber}, {PostalCode} {City}";
  }
}
