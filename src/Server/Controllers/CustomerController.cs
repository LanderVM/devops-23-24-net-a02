using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Shared.Customer;
using Shared.Customers;
using Swashbuckle.AspNetCore.Annotations;

namespace devops_23_24_net_a02.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
  private readonly ILogger<CustomerController> _logger;
  private readonly ICustomerService _customerService;

  public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
  {
    _logger = logger;
    _customerService = customerService;
  }

  [HttpGet]
  [SwaggerOperation("Returns a list of customers")]
  public async Task<CustomerResult.Index> GetIndex([FromQuery] CustomerRequest.Index request)
  {
    return await _customerService.GetIndexAsync(request);
  }

  [HttpPost]
  [SwaggerOperation("Creates a new customer")]
  public async Task<IActionResult> CreateCustomer(CustomerDto.Create model)
  {
    int customerId = await _customerService.CreateAsync(model);
    return CreatedAtAction(nameof(CreateCustomer), new CustomerResponse.Create { CustomerId = customerId });
  }
}
