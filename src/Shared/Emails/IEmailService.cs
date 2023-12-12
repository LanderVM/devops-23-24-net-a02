namespace devops_23_24_net_a02.Shared.Emails;

public interface IEmailService
{
  Task<int> CreateAsync(EmailDto.Create model);

  Task<EmailResult.Index> GetEmailsAsync();
}
