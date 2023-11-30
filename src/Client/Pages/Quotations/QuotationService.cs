using System.Net.Http.Json;
using Shared.Quotations;

namespace devops_23_24_net_a02.Client.Pages.Quotations;

public class QuotationService : IQuotationService
{

  private readonly HttpClient client;
  private const string endpoint = "/api/Quotation";
  public QuotationService(HttpClient client)
  {
    this.client = client;
  }

  public async Task<int> CreateAsync(QuotationDto.Create model)
  {
    throw new NotImplementedException();
  }

  public async Task<QuotationResult.Index> GetIndexAsync()
  {
    var response = await client.GetFromJsonAsync<QuotationResult.Index>(endpoint);
    return response;
  }
}
