namespace devops_23_24_net_a02.Client.EmailOverview;

using System.Net.Http.Json;
using System.Threading.Tasks;
using devops_23_24_net_a02.Shared.Emails;
using shared.GoogleMaps;
using shared.Quotations;

public class EmailService : IEmailService
{

  private readonly HttpClient client;
  private const string endpoint = "/api/email";


  public EmailService(HttpClient client) {
    this.client = client;
  }
  public async Task<int> CreateAsync(EmailDto.Create model)
  {
    var response = await client.PostAsJsonAsync(endpoint,model);
    return await response.Content.ReadFromJsonAsync<int>();
  }

  public async Task<EmailResult.Index> GetEmailsAsync()
  {
    EmailResult.Index? result = await client.GetFromJsonAsync<EmailResult.Index>(endpoint);

    return result!;
  }

  public Task<QuotationResponse.Edit> SendConfirmationMail(QuotationResponse.Create model, GoogleMapsDto.Response distancePrice)
  {
    throw new NotImplementedException();
  }
}

