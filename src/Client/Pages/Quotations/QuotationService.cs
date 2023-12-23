using System.Net.Http.Json;
using System.Text;
using devops_23_24_net_a02.Client.Extensions;
using shared.Equipment;

using shared.Quotations;

namespace devops_23_24_net_a02.Client.Pages.Quotations;

public class QuotationService : IQuotationService
{
  
  private readonly HttpClient publicClient;
  private readonly HttpClient adminClient;
  private const string endpoint = "/api/Quotation";
  
  public QuotationService(IHttpClientFactory  httpClientFactory) {
    adminClient = httpClientFactory.CreateClient("FoodtruckAPI");
    publicClient = httpClientFactory.CreateClient("PublicAPI");
  }

  public async Task<QuotationResult.Index> GetIndexAsync()
  {
    var response = await publicClient.GetFromJsonAsync<QuotationResult.Index>(endpoint);
    return response;
  }

  public async Task<QuotationResult.DetailEdit> GetSpecificDetailEditAsync(int quotationId)
  {
    var response = await adminClient.GetFromJsonAsync<QuotationResult.DetailEdit>($"{endpoint}/{quotationId}");
    return response;
  }

  public async Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync()
  {
    QuotationResult.Detail response = await publicClient.GetFromJsonAsync<QuotationResult.Detail>($"{endpoint}/Estimation/Details");
    return response;
  }

  public async Task<QuotationResult.Calculation> GetPriceEstimationPrice(QuotationDto.Estimate model)
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
    var response = await publicClient.GetFromJsonAsync<QuotationResult.Calculation>($"{endpoint}/Estimation/Calculate?{queryString}");

    return response;
  }

  public async Task<QuotationResult.Dates> GetDatesAsync()
  {
    var response = await publicClient.GetFromJsonAsync<QuotationResult.Dates>($"{endpoint}/Dates");
    return response;
  }

  public async Task<QuotationResult.Create> CreateAsync(QuotationDto.Create model)
  {
    var response = await publicClient.PostAsJsonAsync(endpoint, model);
    return await response.Content.ReadFromJsonAsync<QuotationResult.Create>();
  }

  public async Task<QuotationResponse.Create> UpdateAsync(int QuotationId, QuotationDto.Edit model)
  {
    var response = await adminClient.PutAsJsonAsync($"{endpoint}/{QuotationId}", model);
    return await response.Content.ReadFromJsonAsync<QuotationResponse.Create>();
  }
}
