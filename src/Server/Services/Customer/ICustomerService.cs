using Shared.Customer;
using Shared.Customers;

namespace Server.Services;

public interface ICustomerService
{
  Task<int> CreateAsync(CustomerDto.Create model);
  Task<CustomerResult.Index> GetIndexAsync(CustomerRequest.Index request);

}
