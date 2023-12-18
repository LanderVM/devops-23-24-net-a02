using System.Net.Http.Json;
using System.Text;
using devops_23_24_net_a02.Client.Extensions;
using shared.Quotations;

namespace devops_23_24_net_a02.Client.Pages.Quotations;

public class QuotationService : IQuotationService
{

  private readonly HttpClient client;
  private const string endpoint = "/api/Quotation";
  public QuotationService(HttpClient client)
  {
    this.client = client;
  }

  public async Task<QuotationResult.Create> CreateAsync(QuotationDto.Create model)
  {
    var response = await client.PostAsJsonAsync(endpoint,model);
    return await response.Content.ReadFromJsonAsync<QuotationResult.Create>();
  }

  public async Task<QuotationResult.Index> GetIndexAsync()
  {
    var response = await client.GetFromJsonAsync<QuotationResult.Index>(endpoint);
    return response;
  }

  public async Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync()
  {
    QuotationResult.Detail response = await client.GetFromJsonAsync<QuotationResult.Detail>($"{endpoint}/Estimation/Details");
    return response;
  }

  public async Task<decimal> GetPriceEstimationPrice(QuotationResponse.Estimate model)
  {
    var queryString = new StringBuilder();

    // Add properties to the query string
    queryString.Append($"FormulaId={model.FormulaId}");
    queryString.Append($"&StartTime={model.StartTime}");
    queryString.Append($"&EndTime={model.EndTime}");
    queryString.Append($"&EstimatedNumberOfPeople={model.EstimatedNumberOfPeople}");
    queryString.Append($"&IsTripelBier={model.IsTripelBier}");

    // Handle EquipmentIds
    if (model.EquipmentIds != null && model.EquipmentIds.Any())
    {
      foreach (var equipmentId in model.EquipmentIds)
      {
        queryString.Append($"&EquipmentIds={equipmentId}");
      }
    }
    var response = await client.GetFromJsonAsync<decimal>($"{endpoint}/Estimation/Calculate?{queryString}");

    return response;
  }

  public async Task<QuotationResult.Dates> GetDatesAsync()
  {
    var response = await client.GetFromJsonAsync<QuotationResult.Dates>($"{endpoint}/Dates");
    return response;
  }
}
