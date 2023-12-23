﻿using shared.GoogleMaps;
using shared.Quotations;

namespace devops_23_24_net_a02.Shared.Emails;

public interface IEmailService
{
  Task<int> CreateAsync(EmailDto.Create model);
  Task<QuotationResponse.Edit> SendConfirmationMail(QuotationResponse.Create model, GoogleMapsDto.Response distancePrice);
  Task<EmailResult.Index> GetEmailsAsync();
}
