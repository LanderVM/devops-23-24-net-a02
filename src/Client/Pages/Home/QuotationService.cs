using System.Net.Http.Json;
using shared.Quotations;

namespace devops_23_24_net_a02.Client.Pages.Home;

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

  public async Task<List<DateTime>> GetDatesAsync()
  {
    List<DateTime>? response = await client.GetFromJsonAsync<List<DateTime>>($"/dates");
    return response!;
  }
}

