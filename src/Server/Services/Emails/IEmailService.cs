using devops_23_24_net_a02.Shared.Emails;
using shared.Quotations;

namespace Server.Services;

public interface IEmailService
{
  Task<int> CreateAsync(EmailDto.Create model);
  Task<QuotationResponse.Edit> SendConfirmationMail(QuotationResponse.Create model);
}
