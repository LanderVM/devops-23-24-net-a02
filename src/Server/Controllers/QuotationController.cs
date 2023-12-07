using System.Reflection.Metadata.Ecma335;
using Api.Data.Services.Quotations;
using Microsoft.AspNetCore.Mvc;
using shared.Quotations;
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
  [HttpGet("Estimation/Details")]
  [SwaggerOperation("Returns all required data to set up the calculation for a quotation")]
  public async Task<QuotationResult.Detail> GetEstimatedQuotationDetails()
  {
    return await _quotationService.GetPriceEstimationDetailsAsync();
  }

  [HttpGet("Estimation/Calculate")]
  [SwaggerOperation("Calculates a estimate on how much a offer would cost")]
  public async Task<decimal> GetEstimatedQuotationPrice([FromQuery] QuotationResponse.Estimate model)
  {
    return await _quotationService.GetPriceEstimationPrice(model);
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
    return CreatedAtAction(nameof(RegisterQuotationRequest), quotationId); // TODO new QuotationResult.Create { QuotationId = quotationId}); eens dit gebruikt kan worden op web
  }
  
  [HttpPost("TempAndroid")]
  [SwaggerOperation("TEMPORARY, FIX")]
  public async Task<IActionResult> RegisterQuotationRequestTempAndroid(QuotationDto.Create model)
  {
    _logger.Log(LogLevel.Information,
      "Registering new quotation request at {model} for {(model.Customer.FirstName + model.Customer.LastName)}",
      model.EventLocation, (model.Customer.FirstName + model.Customer.LastName));
    int quotationId = await _quotationService.CreateAsync(model);
    _logger.Log(LogLevel.Information,
      "Registered new quotation request at {model} for {(model.Customer.FirstName + model.Customer.LastName)}",
      model.EventLocation, (model.Customer.FirstName + model.Customer.LastName));
    return CreatedAtAction(nameof(RegisterQuotationRequest), new QuotationResult.Create { QuotationId = quotationId});
  }

  [HttpGet]
  [SwaggerOperation("Gets a list of all the quotations")]
  public async Task<QuotationResult.Index> GetQuotations()
  {
    return await _quotationService.GetIndexAsync();
  }
  [HttpGet("Dates")]
  [SwaggerOperation("Gets all the dates for which there is an approved quotation")]
  public async Task<QuotationResult.Dates> GetApprovedQuotationsDates() { 
    var dateTimes = await _quotationService.GetDatesAsync();
    return dateTimes;
  }

}

