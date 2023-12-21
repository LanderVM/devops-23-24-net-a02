using Microsoft.AspNetCore.Mvc;
using shared.Quotations;
using shared.GoogleMaps;
using Swashbuckle.AspNetCore.Annotations;
using devops_23_24_net_a02.Shared.Emails;

namespace devops_23_24_net_a02.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotationController : ControllerBase
{
  private readonly ILogger<QuotationController> _logger;
  private readonly IQuotationService _quotationService;
  private readonly IGoogleMapsService _googleMapsService;
  private readonly IEmailService _emailService;


  public QuotationController(ILogger<QuotationController> logger, IQuotationService quotationService, IGoogleMapsService googleMapsService, IEmailService emailService)
  {
    _logger = logger;
    _quotationService = quotationService;
    _googleMapsService = googleMapsService;
    _emailService = emailService;

  }

  [HttpGet]
  [SwaggerOperation("Gets a list of all the quotations")]
  public async Task<QuotationResult.Index> GetQuotations()
  {
    return await _quotationService.GetIndexAsync();
  }

  [HttpGet("{QuotationId}")]
  [SwaggerOperation("Gets a list of all the quotations")]
  public async Task<QuotationResult.DetailEdit> GetSpecificDetailQuotation(int QuotationId)
  {
    return await _quotationService.GetSpecificDetailEditAsync(QuotationId);
  }

  [HttpPost]
  [SwaggerOperation("Saves a new quotation offer, registering a new customer if need be")]
  public async Task<IActionResult> RegisterQuotationRequest(QuotationDto.Create model)
  {
    _logger.Log(LogLevel.Information,
      "Registering new quotation request at {model} for {(model.Customer.FirstName + model.Customer.LastName)}",
      model.EventLocation, (model.Customer.FirstName + model.Customer.LastName));
    QuotationResult.Create quotation = await _quotationService.CreateAsync(model);
    _logger.Log(LogLevel.Information,
      "Registered new quotation request at {model} for {(model.Customer.FirstName + model.Customer.LastName)}",
      model.EventLocation, (model.Customer.FirstName + model.Customer.LastName));
    return CreatedAtAction(nameof(RegisterQuotationRequest), quotation); // TODO QuotationResponse ipv QuotationResult teruggeven
  }

  [HttpPut("{QuotationId}")]
  [SwaggerOperation("Changes a quotation offer and send a mail to the costumer")]
  public async Task<QuotationResponse.Edit> UpdateQuotationRequest(int QuotationId, QuotationDto.Edit model)
  {
    var quotation = await _quotationService.UpdateAsync(QuotationId, model);
    var result = await _emailService.SendConfirmationMail(quotation);

    return result;
  }

  [HttpGet("Estimation/Details")]
  [SwaggerOperation("Returns all required data to set up the calculation for a quotation")]
  public async Task<QuotationResult.Detail> GetEstimatedQuotationDetails()
  {
    return await _quotationService.GetPriceEstimationDetailsAsync();
  }

  [HttpGet("Estimation/Calculate")]
  [SwaggerOperation("Calculates a estimate on how much a offer would cost")]
  public async Task<decimal> GetEstimatedQuotationPrice([FromQuery] QuotationDto.Estimate model)
  {
    return await _quotationService.GetPriceEstimationPrice(model);
  }


  [HttpGet("Dates")]
  [SwaggerOperation("Gets all the dates for which there is an approved quotation")]
  public async Task<QuotationResult.Dates> GetApprovedQuotationsDates() { 
    var dateTimes = await _quotationService.GetDatesAsync();
    return dateTimes;
  }

  [HttpGet("DistanctePrice")]
  [SwaggerOperation("Calculates a estimate on how much a offer would cost")]
  public async Task<GoogleMapsDto.Response> GetDistanctePrice([FromQuery] string address)
  {
    return await _googleMapsService.GetDistanceAsync(address);
  }

  
}

