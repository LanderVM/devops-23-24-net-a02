using devops_23_24_net_a02.Shared.DTOs;

namespace Shared.Common;

public abstract class CustomerDto
{
  public class CreateCustomer
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EmailDto Email { get; set; }
    public AddressDto BillingAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string? VatNumber { get; set; }
  }
}
