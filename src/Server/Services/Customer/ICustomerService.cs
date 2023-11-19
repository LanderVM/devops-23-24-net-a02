using Common;
using Shared.Common;

namespace Server.Services;

public interface ICustomerService
{
  Task<int> CreateAsync(CustomerDto.Create model);
}
