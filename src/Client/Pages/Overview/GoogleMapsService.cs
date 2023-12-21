using System.Net.Http.Json;
using System.Text;
using devops_23_24_net_a02.Client.Extensions;
using shared.GoogleMaps;

namespace Pages.Overview;

public class GoogleMapsService : IGoogleMapsService
{

  private readonly HttpClient client;
  private const string endpoint = "/api/Quotation";
  public GoogleMapsService(HttpClient client)
  {
    this.client = client;
  }

  public async Task<GoogleMapsDto.Response> GetDistanceAsync(string request)
  {
    var response = await client.GetFromJsonAsync<GoogleMapsDto.Response>($"{endpoint}/DistanctePrice?address={request}");
    return response;
  }
}
