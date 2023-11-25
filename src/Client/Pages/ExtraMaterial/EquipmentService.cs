﻿using System.Net.Http.Json;
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

  public async Task<int> DeleteAsync(int equipmentId)
  {
    throw new NotImplementedException();
  }

  public async Task<int> CreateAsync(EquipmentDto.Create model)
  {
    var response = await client.PostAsJsonAsync(endpoint,model);
    return await response.Content.ReadFromJsonAsync<int>();
  }

  public async Task<int> UpdateAsync(int equipmentId, EquipmentDto.Mutate model)
  {
    throw new NotImplementedException();
  }

  public Task<EquipmentDto.Index> GetSpecificIndexAsync(int equipmentId)
  {
    throw new NotImplementedException();
  }
}

