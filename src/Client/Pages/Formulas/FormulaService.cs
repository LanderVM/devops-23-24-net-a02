using System.Net.Http.Json;
using shared.Formulas;

namespace devops_23_24_net_a02.Client.Pages.Formulas;

public class FormulaService : IFormulaService
{
  private const string endpoint = "/api/formula";
  private readonly HttpClient adminClient;

  private readonly HttpClient publicClient;

  public FormulaService(IHttpClientFactory httpClientFactory)
  {
    adminClient = httpClientFactory.CreateClient("FoodtruckAPI");
    publicClient = httpClientFactory.CreateClient("PublicAPI");
  }

  public async Task<FormulaResult.Index> GetIndexAsync()
  {
    var response = await publicClient.GetFromJsonAsync<FormulaResult.Index>(endpoint);
    return response;
  }

  public async Task<FormulaDto.Mutate> GetSpecificMutateAsync(int formulaId)
  {
    var response = await adminClient.GetFromJsonAsync<FormulaDto.Mutate>($"{endpoint}/{formulaId}");
    return response;
  }

  public async Task<FormulaResult.Edit> UpdateAsync(int formulaId, FormulaDto.Mutate model)
  {
    var response = await adminClient.PutAsJsonAsync($"{endpoint}/{formulaId}", model);
    return await response.Content.ReadFromJsonAsync<FormulaResult.Edit>();
  }

  public async Task<FormulaResult.EditWithoutImage> UpdateWithoutImageAsync(int formulaId, FormulaDto.Mutate model)
  {
    var response = await adminClient.PutAsJsonAsync($"{endpoint}/WithoutImage/{formulaId}", model);
    return await response.Content.ReadFromJsonAsync<FormulaResult.EditWithoutImage>();
  }
}
