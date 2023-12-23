using System.Net.Http.Json;
using shared.Equipment;

namespace devops_23_24_net_a02.Client.Pages.ExtraMaterial;

public class EquipmentService : IEquipmentService
{
  private const string endpoint = "/api/Equipment";
  private readonly HttpClient adminClient;

  private readonly HttpClient publicClient;

  public EquipmentService(IHttpClientFactory httpClientFactory)
  {
    adminClient = httpClientFactory.CreateClient("FoodtruckAPI");
    publicClient = httpClientFactory.CreateClient("PublicAPI");
  }

  public async Task<EquipmentResult.Index> GetIndexAsync()
  {
    var response = await publicClient.GetFromJsonAsync<EquipmentResult.Index>(endpoint);
    return response;
  }

  public async Task<EquipmentResult.ActiveEquipment> GetActiveEquipmentAsync()
  {
    var response = await publicClient.GetFromJsonAsync<EquipmentResult.ActiveEquipment>($"{endpoint}/active");
    return response;
  }

  public async Task<int> DeleteAsync(int equipmentId)
  {
    var response = await adminClient.DeleteAsync($"{endpoint}/{equipmentId}");
    return await response.Content.ReadFromJsonAsync<int>();
  }

  public async Task<EquipmentResult.Create> CreateAsync(EquipmentDto.Create model)
  {
    var response = await adminClient.PostAsJsonAsync(endpoint, model);
    return await response.Content.ReadFromJsonAsync<EquipmentResult.Create>();
  }

  public async Task<EquipmentResult.CreateWithImage> CreateWithImageAsync(EquipmentDto.Create model)
  {
    var response = await adminClient.PostAsJsonAsync($"{endpoint}/WithImage", model);
    return await response.Content.ReadFromJsonAsync<EquipmentResult.CreateWithImage>();
  }

  public async Task<EquipmentResult.Create> UpdateAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    var response = await adminClient.PutAsJsonAsync($"{endpoint}/{equipmentId}", model);
    return await response.Content.ReadFromJsonAsync<EquipmentResult.Create>();
  }

  public async Task<EquipmentResult.CreateWithImage> UpdateWithImageAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    var response = await adminClient.PutAsJsonAsync($"{endpoint}/WithImage/{equipmentId}", model);
    return await response.Content.ReadFromJsonAsync<EquipmentResult.CreateWithImage>();
  }


  public async Task<EquipmentDto.Mutate> GetSpecificMutateAsync(int equipmentId)
  {
    var response = await adminClient.GetFromJsonAsync<EquipmentDto.Mutate>($"{endpoint}/{equipmentId}");
    return response!;
  }
}
