namespace shared.GoogleMaps;

public interface IGoogleMapsService
{
  public Task<GoogleMapsDto.Response> GetDistanceAsync(string address);
}
