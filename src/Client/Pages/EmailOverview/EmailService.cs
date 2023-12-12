﻿namespace devops_23_24_net_a02.Client.EmailOverview;

using System.Net.Http.Json;
using System.Threading.Tasks;
using devops_23_24_net_a02.Shared.Emails;



public class EmailService : IEmailService
{

  private readonly HttpClient client;
  private const string endpoint = "/api/email";


  public EmailService(HttpClient client) {
    this.client = client;
  }
  public Task<int> CreateAsync(EmailDto.Create model)
  {
    throw new NotImplementedException();
  }

  public async Task<EmailResult.Index> GetEmailsAsync()
  {
    EmailResult.Index? result = await client.GetFromJsonAsync<EmailResult.Index>(endpoint);

    return result!;
  }
}

