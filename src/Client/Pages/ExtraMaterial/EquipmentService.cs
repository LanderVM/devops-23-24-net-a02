using System.Net.Http.Json;
using shared.Equipment;


namespace devops_23_24_net_a02.Client.Pages.ExtraMaterial;

public class EquipmentService : IEquipmentService
{

  private readonly HttpClient client;
  private const string endpoint = "/api/Equipment";
  public EquipmentService(HttpClient client)
  {
    this.client = client;
  }
  public async Task<EquipmentResult.Index> GetIndexAsync()
  {
    var response = await client.GetFromJsonAsync<EquipmentResult.Index>(endpoint);
    return response;
  }
}

