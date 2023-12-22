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
    _logger.Log(LogLevel.Information, "Fetching list of formulas");
    var result = await _quotationService.GetIndexAsync();
    _logger.Log(LogLevel.Information, "Found {result.TotalAmount} formulas", result.TotalAmount);
    return result;
  }

  [HttpGet("{QuotationId}")]
  [SwaggerOperation("Returns a quotation that matches the given id")]
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
    _logger.Log(LogLevel.Information, "Fetching quotation with id {QuotationId} to edit based off model: {model.ToString()}", QuotationId, model.ToString());
    var quotation = await _quotationService.UpdateAsync(QuotationId, model);
    _logger.Log(LogLevel.Information, "Updated quotation with id {QuotationId}: {quotation.ToString()}", QuotationId, quotation.ToString());
    var result = await _emailService.SendConfirmationMail(quotation);
    _logger.Log(LogLevel.Information, "Sending confirmation mail for quotation with id {result.QuotationId}", result.QuotationId);
    return result;
  }

  [HttpGet("Estimation/Details")]
  [SwaggerOperation("Returns all required data to set up the calculation for a quotation")]
  public async Task<QuotationResult.Detail> GetEstimatedQuotationDetails()
  {
    _logger.Log(LogLevel.Information, "Fetching data for estimation screen");
    return await _quotationService.GetPriceEstimationDetailsAsync();
  }

  [HttpGet("Estimation/Calculate")]
  [SwaggerOperation("Calculates a estimate on how much a offer would cost")]
  public async Task<QuotationResult.Calculation> GetEstimatedQuotationPrice([FromQuery] QuotationDto.Estimate model)
  {
    _logger.Log(LogLevel.Information, "Calculating estimated price based off model: {model.ToString()}", model.ToString());
    var result = await _quotationService.GetPriceEstimationPrice(model);
    _logger.Log(LogLevel.Information, "Calculated estimated price of {result.EstimatedPrice} based off model: {model.ToString()}", result.EstimatedPrice, model.ToString());
    return result;
  }


  [HttpGet("Dates")]
  [SwaggerOperation("Gets all the dates for which there is an approved quotation")]
  public async Task<QuotationResult.Dates> GetApprovedQuotationsDates() { 
    _logger.Log(LogLevel.Information, "Fetching unavailable date ranges");
    var dateTimes = await _quotationService.GetDatesAsync();
    _logger.Log(LogLevel.Information, "Fetched unavailable date ranges: {dateTimes.DateRanges.ToString()}", dateTimes.DateRanges.ToString());
    return dateTimes;
  }

  [HttpGet("PriceDistance")]
  [SwaggerOperation("Calculates a estimate on how much a offer would cost")]
  public async Task<GoogleMapsDto.Response> GetDistanctePrice([FromQuery] string address)
  {
    _logger.Log(LogLevel.Information, "Calculating estimated transport price for address: {address}", address);
    var result = await _googleMapsService.GetDistanceAsync(address);
    _logger.Log(LogLevel.Information, "Calculated estimated transport price of {result.PricePerKm * result.DistanceAmount} before reduction for address: {address}", result.PricePerKm * result.DistanceAmount, address);
    return result;
  }

  
}

