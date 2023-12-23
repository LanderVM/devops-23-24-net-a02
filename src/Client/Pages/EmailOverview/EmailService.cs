using System.Net.Http.Json;
using devops_23_24_net_a02.Shared.Emails;
using shared.GoogleMaps;
using shared.Quotations;

namespace devops_23_24_net_a02.Client.EmailOverview;

public class EmailService : IEmailService
{
  private const string endpoint = "/api/email";
  private readonly HttpClient adminClient;

  private readonly HttpClient publicClient;


  public EmailService(IHttpClientFactory httpClientFactory)
  {
    adminClient = httpClientFactory.CreateClient("FoodtruckAPI");
    publicClient = httpClientFactory.CreateClient("PublicAPI");
  }

  public async Task<int> CreateAsync(EmailDto.Create model)
  {
    var response = await publicClient.PostAsJsonAsync(endpoint, model);
    return await response.Content.ReadFromJsonAsync<int>();
  }

  public async Task<EmailResult.Index> GetEmailsAsync()
  {
    var result = await adminClient.GetFromJsonAsync<EmailResult.Index>(endpoint);
    return result!;
  }

  public Task<QuotationResponse.Edit> SendConfirmationMail(QuotationResponse.Create model,
    GoogleMapsDto.Response distancePrice)
  {
    throw new NotImplementedException();
  }

  public Task SendQuotationMail(QuotationResponse.Create model, GoogleMapsDto.Response distancePrice)
  {
    throw new NotImplementedException();
  }
}
