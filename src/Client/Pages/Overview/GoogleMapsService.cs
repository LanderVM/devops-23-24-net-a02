using System.Net.Http.Json;
using shared.GoogleMaps;

namespace Pages.Overview;

public class GoogleMapsService : IGoogleMapsService
{
  private const string endpoint = "/api/Quotation";

  private readonly HttpClient client;

  public GoogleMapsService(HttpClient client)
  {
    this.client = client;
  }

  public async Task<GoogleMapsDto.Response> GetDistanceAsync(string request)
  {
    var response = await client.GetFromJsonAsync<GoogleMapsDto.Response>($"{endpoint}/PriceDistance?address={request}");
    return response;
  }
}
