namespace Shared.Common;

public abstract class AddressDto
{
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}
