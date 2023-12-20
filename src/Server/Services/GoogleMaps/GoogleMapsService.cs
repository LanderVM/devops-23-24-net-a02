using System.Text.Json;
using shared.GoogleMaps;
using Shared.Common;

namespace server.Services;
public class GoogleMapsService : IGoogleMapsService
{
  private readonly string connectionString;

  public GoogleMapsService(IConfiguration configuration) 
  {
    connectionString = configuration.GetConnectionString("GoogleMaps");
  }

  public async Task<GoogleMapsDto.Response> GetDistanceAsync(string address)
  {
    HttpClient client = new HttpClient();
    string base_Url = "https://maps.googleapis.com/maps/api/distancematrix/json?";
    string origins = "origins=Arbeidstraat 14, Aalst";
    string region = "region=be";
    string apiKey = $"key={connectionString}";
    string destination = $"destinations={address}";

    string URL = base_Url + origins + "&" + destination + "&" + region + "&" + apiKey;

    HttpResponseMessage response = await client.GetAsync(URL);
    var responseContent = await response.Content.ReadAsStringAsync();

    GoogleMapsDto.DistanceMatrixResponse? index = JsonSerializer.Deserialize<GoogleMapsDto.DistanceMatrixResponse>(responseContent, new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    });

    if (index == null || index.Rows == null || index.Rows.Count == 0 || index.Rows[0].Elements == null || index.Rows[0].Elements.Count == 0)
    {
      throw new ArgumentException($"Invalid or empty response");
    }

    GoogleMapsDto.Response result = new GoogleMapsDto.Response
    {
      DistanceAmount = index.Rows[0].Elements[0].Distance.Value / 1000,
      PricePerKm = 0.75M   
    };

    return result;
  }
}
