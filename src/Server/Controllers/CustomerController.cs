using System.Data;
using Common;
using devops_23_24_net_a02.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Shared.Common;
using Shared.Emails;
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

  [HttpPost]
  [SwaggerOperation("Creates a new customer")]
  public async Task<IActionResult> CreateCustomer(CustomerDto.Create model)
  {
    int customerId = await _customerService.CreateAsync(model);
    return CreatedAtAction(nameof(CreateCustomer), new CustomerResponse.Create { CustomerId = customerId });
  }
}
