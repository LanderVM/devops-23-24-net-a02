using Shared.Customer;

namespace devops_23_24_net_a02.Client;

public class PersonalDetailsState
{
  public CustomerDto.Create Customer { get; set; } = new();

  public void Clear()
  {
    Customer = new CustomerDto.Create();
  }
}
