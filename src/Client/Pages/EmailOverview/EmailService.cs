
namespace devops_23_24_net_a02.Client.EmailOverview;

using System.Net.Http.Json;
using System.Threading.Tasks;
using devops_23_24_net_a02.Shared.Emails;
using shared.Quotations;

public class EmailService : IEmailService
{

  private readonly HttpClient publicClient;
  private readonly HttpClient adminClient;
  private const string endpoint = "/api/email";


  public EmailService(IHttpClientFactory  httpClientFactory) {
    adminClient = httpClientFactory.CreateClient("FoodtruckAPI");
    publicClient = httpClientFactory.CreateClient("PublicAPI");
  }
  
  public async Task<int> CreateAsync(EmailDto.Create model)
  {
    var response = await publicClient.PostAsJsonAsync(endpoint,model);
    return await response.Content.ReadFromJsonAsync<int>();
  }

  public async Task<EmailResult.Index> GetEmailsAsync()
  {
    EmailResult.Index? result = await adminClient.GetFromJsonAsync<EmailResult.Index>(endpoint);
    return result!;
  }

  public Task<QuotationResponse.Edit> SendConfirmationMail(QuotationResponse.Create model)
  {
    throw new NotImplementedException();
  }
}

