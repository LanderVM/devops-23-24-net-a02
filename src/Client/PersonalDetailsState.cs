using FluentValidation;
using shared;
using Shared.Customer;
using devops_23_24_net_a02.Shared.Emails;
using Shared.Common;

namespace devops_23_24_net_a02.Client;

public class PersonalDetailsState
{ 
  public CustomerDto.Create Customer { get; set; } = new CustomerDto.Create();

  public void Clear() {
    Customer = new CustomerDto.Create();
  }

}
