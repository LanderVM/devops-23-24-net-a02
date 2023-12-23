using System.Text.Json;
using shared.GoogleMaps;

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
    var client = new HttpClient();
    var base_Url = "https://maps.googleapis.com/maps/api/distancematrix/json?";
    var origins = "origins=Arbeidstraat 14, Aalst";
    var region = "region=be";
    var apiKey = $"key={connectionString}";
    var destination = $"destinations={address}";

    var URL = base_Url + origins + "&" + destination + "&" + region + "&" + apiKey;

    var response = await client.GetAsync(URL);
    var responseContent = await response.Content.ReadAsStringAsync();

    var index = JsonSerializer.Deserialize<GoogleMapsDto.DistanceMatrixResponse>(responseContent,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    if (index == null || index.Rows == null || index.Rows.Count == 0 || index.Rows[0].Elements == null ||
        index.Rows[0].Elements.Count == 0)
    {
      throw new ArgumentException("Invalid or empty response");
    }

    var result = new GoogleMapsDto.Response
    {
      DistanceAmount = index.Rows[0].Elements[0].Distance.Value / 1000, PricePerKm = 0.75M
    };

    return result;
  }
}
