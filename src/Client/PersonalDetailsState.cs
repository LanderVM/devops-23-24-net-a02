using FluentValidation;
using shared;
using Shared.Customer;
using devops_23_24_net_a02.Shared.Emails;
using Shared.Common;

namespace devops_23_24_net_a02.Client;

public class PersonalDetailsState
{
  /*
  public CustomerDto.Create customer = new CustomerDto.Create { 
    FirstName="",
    LastName="",
    PhoneNumber="",
    VatNumber="",
    Email= new EmailDto.Create { 
     Email=""
    },
    BillingAddress = new AddressDto { 
     Street ="",
     City = "",
     PostalCode = "",
     HouseNumber = "",
    }

  };*/
  public CustomerDto.Create customer = new();

}
