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
  public async Task<EquipmentResult.ActiveEquipment> GetActiveEquipmentAsync()
  {
    var response = await client.GetFromJsonAsync<EquipmentResult.ActiveEquipment>($"{endpoint}/active");
    return response;
  }

  public async Task<int> DeleteAsync(int equipmentId)
  {
    var response = await client.DeleteAsync($"{endpoint}/{equipmentId}");
    return await response.Content.ReadFromJsonAsync<int>();
  }

  public async Task<EquipmentResult.Create> CreateAsync(EquipmentDto.Create model)
  {
    var response = await client.PostAsJsonAsync(endpoint,model);
    return await response.Content.ReadFromJsonAsync<EquipmentResult.Create>();
  }

  public async Task<EquipmentResult.CreateWithImage> CreateWithImageAsync(EquipmentDto.Create model)
  {
    var response = await client.PostAsJsonAsync($"{endpoint}/WithImage", model);
    return await response.Content.ReadFromJsonAsync<EquipmentResult.CreateWithImage>();
  }

  public async Task<EquipmentResult.Create> UpdateAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    var response = await client.PutAsJsonAsync($"{endpoint}/{equipmentId}", model);
    return await response.Content.ReadFromJsonAsync<EquipmentResult.Create>();
  }

  public async Task<EquipmentResult.CreateWithImage> UpdateWithImageAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    var response = await client.PutAsJsonAsync($"{endpoint}/WithImage/{equipmentId}", model);
    return await response.Content.ReadFromJsonAsync<EquipmentResult.CreateWithImage>();
  }

  public async Task<EquipmentDto.Index> GetSpecificIndexAsync(int equipmentId)
  {
    EquipmentDto.Index? response = await client.GetFromJsonAsync<EquipmentDto.Index>($"{endpoint}/{equipmentId}");
    return response!;
  }
  
  public async Task<EquipmentDto.Mutate> GetSpecificMutateAsync(int equipmentId)
  {
    EquipmentDto.Mutate? response = await client.GetFromJsonAsync<EquipmentDto.Mutate>($"{endpoint}/{equipmentId}");
    return response!;
  }

  
}

