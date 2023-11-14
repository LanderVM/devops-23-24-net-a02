using System.Net.Http.Json;
using shared.Equipment;
using shared.Formulas;

namespace devops_23_24_net_a02.Client.Pages.Formulas;
public class FormulaService : IFormulaService { 

  private readonly HttpClient client;
  private const string endpoint = "/api/formula";

  public FormulaService(HttpClient client)
  {
    this.client = client;
  }

  public async Task<FormulaResult.Index> GetIndexAsync()
  {
    FormulaResult.Index response = await client.GetFromJsonAsync<FormulaResult.Index>(endpoint);
    return response;
  }
}

