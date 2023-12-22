using System.Net.Http.Json;
using shared.Formulas;
namespace devops_23_24_net_a02.Client.Pages.Formulas
{
  public class FormulaService : IFormulaService
  {
    
    private readonly HttpClient publicClient;
    private readonly HttpClient adminClient;
    private const string endpoint = "/api/formula";

    public FormulaService(IHttpClientFactory  httpClientFactory) {
      adminClient = httpClientFactory.CreateClient("FoodtruckAPI");
      publicClient = httpClientFactory.CreateClient("PublicAPI");
    }

    public async Task<FormulaResult.Index> GetIndexAsync()
    {
      FormulaResult.Index response = await publicClient.GetFromJsonAsync<FormulaResult.Index>(endpoint);
      return response;
    }

    public async Task<FormulaDto.Mutate> GetSpecificMutateAsync(int formulaId)
    {
      FormulaDto.Mutate? response = await publicClient.GetFromJsonAsync<FormulaDto.Mutate>($"{endpoint}/{formulaId}");
      return response;
    }

    public async Task<FormulaResult.Edit> UpdateAsync(int formulaId, FormulaDto.Mutate model)
    {
      var response = await adminClient.PutAsJsonAsync($"{endpoint}/{formulaId}", model);
      return await response.Content.ReadFromJsonAsync<FormulaResult.Edit>();
    }
  }
}
