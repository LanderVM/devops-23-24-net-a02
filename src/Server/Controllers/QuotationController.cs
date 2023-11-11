using Microsoft.AspNetCore.Mvc;
using Shared.Quotations;
using Swashbuckle.AspNetCore.Annotations;

namespace devops_23_24_net_a02.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotationController : ControllerBase
{
  private readonly ILogger<QuotationController> _logger;
  private readonly IQuotationService _quotationService;

  public QuotationController(ILogger<QuotationController> logger, IQuotationService quotationService)
  {
    _logger = logger;
    _quotationService = quotationService;
  }

  [HttpPost]
  [SwaggerOperation("Saves a new quotation offer, registering a new customer if need be")]
  public async Task<IActionResult> RegisterQuotationRequest(QuotationDto.Create model)
  {
    _logger.Log(LogLevel.Information,
      "Registering new quotation request at {model} for {(model.Customer.FirstName + model.Customer.LastName)}",
      model.EventLocation, (model.Customer.FirstName + model.Customer.LastName));
    int quotationId = await _quotationService.CreateAsync(model);
    _logger.Log(LogLevel.Information,
      "Registered new quotation request at {model} for {(model.Customer.FirstName + model.Customer.LastName)}",
      model.EventLocation, (model.Customer.FirstName + model.Customer.LastName));
    return CreatedAtAction(nameof(RegisterQuotationRequest), new QuotationResponse.Create { QuotationId = quotationId});
  }
}
